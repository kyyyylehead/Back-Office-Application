using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlzLogin.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost("/account/login")]
        public async Task<IActionResult> Login(UserCredentials credentials)
        {
            string user = "uid=" + credentials.Username + ",dc=example,dc=com";
            string pass = credentials.Password;
            //using (DirectoryEntry entry = new DirectoryEntry("LDAP://ldap.forumsys.com:389/dc=example,dc=com", "cn=read-only-admin,dc=example,dc=com", "password", AuthenticationTypes.ServerBind))
            using (DirectoryEntry entry = new DirectoryEntry("LDAP://ldap.forumsys.com:389/dc=example,dc=com", user, pass, AuthenticationTypes.None))
            {
                try
                {
                    DirectorySearcher search = new DirectorySearcher(entry)
                    { 
                        Filter = "(uid=" + credentials.Username + ")" 
                    };
                    search.PropertiesToLoad.Add("cn");
                    SearchResult result = search.FindOne();

                    if (result != null)
                    {
                        string role = "user";

                            /*
                            ResultPropertyCollection fields = roleResults.Properties;
                            foreach (String ldapField in fields.PropertyNames)
                            {
                                foreach (Object myCollection in fields[ldapField])
                                {
                                    if (ldapField == "employeetype")
                                        role = myCollection.ToString().ToLower();
                                }
                            }
                            */

                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, credentials.Username),
                            new Claim(ClaimTypes.Role, role)
                        };
    
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                               
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                               
                        return LocalRedirect("/");
                    }
                    else  return LocalRedirect("/login/Invalid credentials");
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return LocalRedirect("/login/Invalid credentials");
                }
            }
        }

        [HttpGet("/account/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }

        private string GetAuthenticatingDirectory(string domain)
        {
            string authenticatingDirectory = string.Empty;
            string dotComDomain = domain + @".com";

            // Connect to RootDSE
            using (DirectoryEntry RootDSE = new DirectoryEntry("LDAP://rootDSE"))
            {
                // Retrieve the Configuration Naming Context from RootDSE
                string configNC = RootDSE.Properties["configurationNamingContext"].Value.ToString();

                // Connect to the Configuration Naming Context
                using (DirectoryEntry configSearchRoot = new DirectoryEntry("LDAP://" + configNC))
                {
                    // Search for all partitions where the NetBIOSName is set.
                    using (DirectorySearcher configSearch = new DirectorySearcher(configSearchRoot))
                    {
                        configSearch.Filter = ("(NETBIOSName=*)");

                        // Configure search to return dnsroot and ncname attributes
                        configSearch.PropertiesToLoad.Add("dnsroot");
                        configSearch.PropertiesToLoad.Add("ncname");
                        using (SearchResultCollection forestPartitionList = configSearch.FindAll())
                        {
                            // Loop through each returned domain in the result collection
                            foreach (SearchResult domainPartition in forestPartitionList)
                            {
                                // domainName like "domain.com". ncName like "DC=domain,DC=com"
                                string domainName = domainPartition.Properties["dnsroot"][0].ToString();
                                string ncName = domainPartition.Properties["ncname"][0].ToString();

                                if (dotComDomain.Equals(domainName, StringComparison.OrdinalIgnoreCase))
                                {
                                    authenticatingDirectory = ncName;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return authenticatingDirectory;
        }
    }

    public class UserCredentials
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

