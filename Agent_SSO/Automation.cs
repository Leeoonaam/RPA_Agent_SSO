using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium.Remote;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Net;
using System.Xml;
using System.Globalization;
using System.Diagnostics;
using System.Net.Sockets;



/// <summary>
/// Agent_SSO
/// </summary>
namespace Agent_SSO
{
    public class Automation
    {

        ChromeDriver obj;
        clsautom ls = new clsautom();
        int PortalSimples = 0;
        int CRM = 0;
        int CIM = 0;
        int SKORE = 0;
        int INTERCON = 0;
        int SALESFORCE = 0;
        int GENESYS = 0;
        int TESTE1 = 0;


        /// <summary>
        /// Compare
        /// </summary>
        /// <param name="listedId"></param>
        /// <param name="runtimeId"></param>
        /// <returns></returns>
        internal static bool Compare(int[] listedId, object runtimeId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// InicializaChromeDriver
        /// </summary>
        public void InicializaChromeDriver()
        {
            try
            {

                // inicializa o chrome service
                var driverService = ChromeDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;

                // inicializa o chrome options
                ChromeOptions chromeOptions = new ChromeOptions();

                chromeOptions.AddExcludedArgument("--single-process");
                chromeOptions.AddExcludedArgument("enable-automation");
                chromeOptions.AddAdditionalCapability("useAutomationExtension", false);

                //NÃO SALVA SENHAS
                chromeOptions.AddUserProfilePreference("credentials_enable_service", false);

                obj = new ChromeDriver(driverService, chromeOptions);


                // AUMENTA A TELA
                obj.Manage().Window.Maximize();
            }
            catch (Exception err)
            {
                MessageBox.Show("Erro: " + err.Message);
                return;
            }
        }

        /// <summary>
        /// FechaChromeDriver
        /// </summary>
        public void FechaChromeDriver()
        {
            try
            {
                obj.Dispose();
            }
            catch (Exception)
            {
            }
        }



        /// <summary>
        /// RealizaLoginPortalSimples
        /// </summary>
        /// <param name="sLogin"></param>
        /// <param name="sSenha"></param>
        public int RealizaLoginPortalSimples(string sLogin, string sSenha, int Logado)
        {
            try
            {
                DateTime dt;
                Boolean Sucess = false;

                  //FECHA TODOS OS SITES
                if (Logado == 0)
                {
                    //MATA PROCESSOS CHROMEDRIVE
                    Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");

                    foreach (var chromeDriverProcess in chromeDriverProcesses)
                    {
                        chromeDriverProcess.Kill();
                    }

                    //KILL
                    Process oNice = Process.Start(@"c:\\temp\\kill_chrome.bat");

                    return 0;
                }


                try
                {
                    if (obj.WindowHandles.Count == 0)
                    {
                        //ERRO
                        //return 1;
                    }
                    else
                    {
                        obj.SwitchTo().Window(obj.WindowHandles[0]);
                    }
                }
                catch (Exception)
                {
                    //REABRE PORTAL SIMPLES
                    InicializaChromeDriver();
                }


                for (int x = 0; x < obj.WindowHandles.Count; x++)
                {
                    obj.SwitchTo().Window(obj.WindowHandles[x]);

                    if (obj.Title == "Portal Simples")
                    {

                        //VERIFICA SE O AGENTE JÁ ESTÁ NA TELA INICIAL
                        Sucess = WaitObject("username-field", "id");

                        if (Sucess == true)
                        {
                            //NÃO ESTA EM TELA INICIAL
                            goto RealizaLogin;

                        }
                        else
                        {
                            //ESTÁ EM TELA INICIAL - ABRE TODOS OS SISTEMAS LEGADOS
                            return 0;

                        }

                    }
                    Application.DoEvents();
                }


                if (obj.WindowHandles.Count == 1 && obj.Title == "")
                {
                    goto RealizaLogin;
                }
                else
                {

                    ls.SendKeys("^t", "xx0000Y127858#xF");

                    Thread.Sleep(200);

                    if (obj.WindowHandles.Count == 1)
                    {
                        // SETA ABA
                        obj.SwitchTo().Window(obj.WindowHandles[obj.WindowHandles.Count]);
                    }
                    else
                    {
                        // SETA ULTIMA ABA
                        obj.SwitchTo().Window(obj.WindowHandles[obj.WindowHandles.Count - 1]);
                    }

                    // VERIRICA SE PAGINA ACABOU DE ABRIR
                    if (obj.Title == "")
                    {
                        goto RealizaLogin;
                    }

                }

            RealizaLogin:

                // CHAMA URL DO LEGADO PORTAL SIMPLES
                obj.Navigate().GoToUrl("https://simples.bancointer.com.br/WebPortal/");

                Thread.Sleep(3000);

                dt = DateTime.Now;

                //VERIFICA SE O AGENTE JÁ ESTÁ NA TELA PRINCIPAL
                Sucess = WaitObject("username-field", "id");

                if (Sucess == false)
                {
                    // FIM
                    return 3;
                }

                dt = DateTime.Now;

                //PREENCHE LOGIN
                List<IWebElement> elementLogin = new List<IWebElement>();
                elementLogin.AddRange(obj.FindElements(By.Id("username-field")));

                if (elementLogin.Count <= 0)
                {
                    while (elementLogin.Count <= 0)
                    {
                        elementLogin.AddRange(obj.FindElements(By.Id("username-field")));
                        Application.DoEvents();

                        if ((DateTime.Now - dt).TotalSeconds > 40)
                        {
                            // ERRO
                            return 1;
                        }
                    }

                    elementLogin[0].SendKeys(sLogin);
                }
                else
                {
                    elementLogin[0].SendKeys(sLogin);

                    while (elementLogin[0].GetAttribute("value") == "")
                    {
                        elementLogin.AddRange(obj.FindElements(By.Id("username-field")));
                        elementLogin[0].SendKeys(sLogin);
                        Application.DoEvents();
                    }

                }

                Thread.Sleep(150);

                //PRRENCHE SENHA
                List<IWebElement> elementSenha = new List<IWebElement>();
                elementSenha.AddRange(obj.FindElements(By.Id("password-field")));

                if (elementSenha.Count <= 0)
                {
                    while (elementSenha.Count <= 0)
                    {
                        elementSenha.AddRange(obj.FindElements(By.Id("password-field")));
                        Application.DoEvents();

                        if ((DateTime.Now - dt).TotalSeconds > 10)
                        {
                            //erro
                            return 1;
                        }
                    }
                    elementSenha[0].SendKeys(sSenha);

                }
                else
                {
                    elementSenha[0].SendKeys(sSenha);
                }

                Thread.Sleep(150);

                // CLICA EM ENTRAR
                List<IWebElement> elementEntrar = new List<IWebElement>();
                elementEntrar.AddRange(obj.FindElements(By.Id("login-btn")));

                if (elementEntrar.Count > 0)
                {
                    elementEntrar[0].Submit();
                }


                Thread.Sleep(1000);


                //VERIFICA TELA PRINCÍPAL
                Sucess = WaitObject("appList", "id");

                if (Sucess == true)
                {

                    //ESTÁ EM TELA INICIAL - FIM DE PROCESSO
                    return 3;
                }
                else
                {
                    //NÃO ESTÁ LOGADAR NOVAMENTE
                    goto RealizaLogin;
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("Erro: " + err.Message);
                return 1;
            }
        }

        /// <summary>
        /// RealizaLoginPortalSimples
        /// </summary>
        /// <param name="sLogin"></param>
        /// <param name="sSenha"></param>
        public int RealizaLoginPortalSimples_Debug(string sLogin, string sSenha)
        {
            try
            {
                DateTime dt;
                Boolean Sucess = false;

                try
                {
                    if (obj.WindowHandles.Count == 0)
                    {
                        //ERRO
                        //return 1;
                    }
                    else
                    {
                        obj.SwitchTo().Window(obj.WindowHandles[0]);
                    }
                }
                catch (Exception)
                {
                    InicializaChromeDriver();
                }


                for (int x = 0; x < obj.WindowHandles.Count; x++)
                {
                    obj.SwitchTo().Window(obj.WindowHandles[x]);

                    if (obj.Title == "Portal Simples")
                    {

                        //VERIFICA SE O AGENTE JÁ ESTÁ NA TELA INICIAL
                        Sucess = WaitObject("username-field", "id");

                        if (Sucess == true)
                        {
                            //NÃO ESTA EM TELA INICIAL
                            goto RealizaLogin;

                        }
                        else
                        {
                            //ESTÁ EM TELA INICIAL - ABRE TODOS OS SISTEMAS LEGADOS
                            return 0;

                        }

                    }
                    Application.DoEvents();
                }


                if (obj.WindowHandles.Count == 1 && obj.Title == "")
                {
                    goto RealizaLogin;
                }
                else
                {

                    ls.SendKeys("^t", "xx0000Y127858#xF");

                    Thread.Sleep(200);

                    if (obj.WindowHandles.Count == 1)
                    {
                        // SETA ABA
                        obj.SwitchTo().Window(obj.WindowHandles[obj.WindowHandles.Count]);
                    }
                    else
                    {
                        // SETA ULTIMA ABA
                        obj.SwitchTo().Window(obj.WindowHandles[obj.WindowHandles.Count - 1]);
                    }

                    // VERIRICA SE PAGINA ACABOU DE ABRIR
                    if (obj.Title == "")
                    {
                        goto RealizaLogin;
                    }

                }

            RealizaLogin:

                // CHAMA URL DO LEGADO PORTAL SIMPLES
                obj.Navigate().GoToUrl("https://simples.bancointer.com.br/WebPortal/");

                Thread.Sleep(3000);

                dt = DateTime.Now;

                //VERIFICA SE O AGENTE JÁ ESTÁ NA TELA PRINCIPAL
                Sucess = WaitObject("username-field", "id");

                if (Sucess == false)
                {
                    // FIM
                    return 3;
                }

                dt = DateTime.Now;

                //PREENCHE LOGIN
                List<IWebElement> elementLogin = new List<IWebElement>();
                elementLogin.AddRange(obj.FindElements(By.Id("username-field")));

                if (elementLogin.Count <= 0)
                {
                    while (elementLogin.Count <= 0)
                    {
                        elementLogin.AddRange(obj.FindElements(By.Id("username-field")));
                        Application.DoEvents();

                        if ((DateTime.Now - dt).TotalSeconds > 40)
                        {
                            // ERRO
                            return 1;
                        }
                    }

                    elementLogin[0].SendKeys(sLogin);
                }
                else
                {
                    elementLogin[0].SendKeys(sLogin);

                    while (elementLogin[0].GetAttribute("value") == "")
                    {
                        elementLogin.AddRange(obj.FindElements(By.Id("username-field")));
                        elementLogin[0].SendKeys(sLogin);
                        Application.DoEvents();
                    }

                }

                Thread.Sleep(150);

                //PRRENCHE SENHA
                List<IWebElement> elementSenha = new List<IWebElement>();
                elementSenha.AddRange(obj.FindElements(By.Id("password-field")));

                if (elementSenha.Count <= 0)
                {
                    while (elementSenha.Count <= 0)
                    {
                        elementSenha.AddRange(obj.FindElements(By.Id("password-field")));
                        Application.DoEvents();

                        if ((DateTime.Now - dt).TotalSeconds > 10)
                        {
                            //erro
                            return 1;
                        }
                    }
                    elementSenha[0].SendKeys(sSenha);

                }
                else
                {
                    elementSenha[0].SendKeys(sSenha);
                }

                Thread.Sleep(150);

                return 3;

            }
            catch (Exception err)
            {
                MessageBox.Show("Erro: " + err.Message);
                return 1;
            }
        }



        /// <summary>
        /// VerificaPaginaAbertas
        /// </summary>
        /// <param name="sLogin"></param>
        /// <param name="sSenha"></param>
        public int VerificaPaginaAbertas(int Logado)
        {
            try
            {
                DateTime dt;
                Boolean Sucess = false;
                Boolean Aguarde_Text = false;
                string Titulo = "";

                //Limpa variável de verificação
                PortalSimples = 0;
                SALESFORCE = 0;
                GENESYS = 0;
                INTERCON = 0;
                Titulo = "";

                //FECHA TODOS OS SITES
                if (Logado == 0)
                {
                    //MATA PROCESSOS CHROMEDRIVE
                    Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");

                    foreach (var chromeDriverProcess in chromeDriverProcesses)
                    {
                        chromeDriverProcess.Kill();
                    }

                    //KILL
                    Process oNice = Process.Start(@"c:\\temp\\kill_chrome.bat");

                    return 0;
                }

                //INICIA PROCESSO DE VERIFICAÇÃO
                try
                {
                   
                    //VERIFICA SE POSSUE ALGUMA JANELA ABERTA
                    if (obj.WindowHandles.Count == 0)
                    {
                        //NENHUMA PAGINA ABERTA ABRE PORTAL SIMPLES
                        return 3;
                    }
                    else
                    {
                        //PRIMEIRA JENELA
                        obj.SwitchTo().Window(obj.WindowHandles[0]);
                    }
                }
                catch (Exception)
                {

                    //MATA PROCESSOS CHROMEDRIVE
                    Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");

                    foreach (var chromeDriverProcess in chromeDriverProcesses)
                    {
                        chromeDriverProcess.Kill();
                    }

                    //KILL
                    Process oNice = Process.Start(@"c:\\temp\\kill_chrome.bat");


                    //INICIA CHROME
                    InicializaChromeDriver();
                }


                //EFETUA LEITURA DE JANELA ABERTA
                for (int x = 0; x < obj.WindowHandles.Count; x++)
                {
                    obj.SwitchTo().Window(obj.WindowHandles[x]);

                    Titulo = obj.Title.ToString();

                    //VERIFICA SISTES ABERTOS
                    if (obj.Title == "Portal Simples")
                    {
                        PortalSimples = 1;

                        //VERIFICA SE O AGENTE JÁ ESTÁ NA TELA PRINCIPAL
                        Sucess = WaitObject("username-field", "id");

                        if (Sucess == false)
                        {
                            PortalSimples = 1;
                        }
                        else
                        {
                            PortalSimples = 0;
                        }

                    }


                    if (Titulo.ToUpper().IndexOf(("BancoInter").ToUpper()) > -1 || Titulo.ToUpper().IndexOf(("Genesys").ToUpper()) > -1 || Titulo.ToUpper().IndexOf(("FLX").ToUpper()) > -1)
                    {
                        GENESYS = 1;
                    }


                    if (Titulo.ToUpper().IndexOf(("Salesforce").ToUpper()) > -1 || obj.Title == "Lightning Experience")
                    {
                        SALESFORCE = 1;
                    }

                    if (obj.Title == "Intercom | The easiest way to see and talk to your users" || obj.Title == "Users | Inter | Intercom" || Titulo.ToUpper().IndexOf(("Intercom").ToUpper()) > -1 || Titulo.ToUpper().IndexOf(("articles").ToUpper()) > -1)
                    {
                        INTERCON = 1;
                    }


                    Application.DoEvents();
                }

                //ABRE JANELA FECHADAS
                if (PortalSimples == 0)
                {

                    try
                    {

                        //MATA PROCESSOS CHROMEDRIVE
                        Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");

                        foreach (var chromeDriverProcess in chromeDriverProcesses)
                        {
                            chromeDriverProcess.Kill();
                        }

                        //KILL
                        Process oNice = Process.Start(@"c:\\temp\\kill_chrome.bat");
                    }
                    catch (Exception)
                    {

                    }

                    //ABRE PORTAL SIMPLES PARA LOGAR
                    return 3;
                }


                //ABRE NOVAMENTE PAGINA DO GENESYS
                if (GENESYS == 0)
                {

                    obj.ExecuteScript("window.open('https://login.mypurecloud.com/#/splash','_blank');");

                    Aguarde_Text = false;

                    //VERIFICA SE ESTÁ NA PAGINA PRINCIPAL
                    while (Aguarde_Text == false)
                    {
                        Aguarde_Text = Wait_Text("Collaborate/ Communicate");

                        //SE NÃO IDENTIFICAR PAGINA DEFAULT - REALIZA NOVAMENTE
                        if (Aguarde_Text == true)
                        {
                            Thread.Sleep(1);
                        }

                    }

                    for (int xxx = 0; xxx < obj.WindowHandles.Count; xxx++)
                    {
                        obj.SwitchTo().Window(obj.WindowHandles[xxx]);

                        Titulo = obj.Title.ToString();

                        if (Titulo.ToUpper().IndexOf(("BancoInter").ToUpper()) > -1 || Titulo.ToUpper().IndexOf(("Genesys").ToUpper()) > -1 || Titulo.ToUpper().IndexOf(("FLX").ToUpper()) > -1)
                        {
                            obj.Navigate().GoToUrl("https://apps.mypurecloud.com/directory");

                            break;
                        }

                    }

                    Thread.Sleep(250);

                }



                //VOLTA TELA PORTAL SIMPLES
                for (int x = 0; x < obj.WindowHandles.Count; x++)
                {
                    obj.SwitchTo().Window(obj.WindowHandles[x]);

                    //VERIFICA SISTES ABERTOS
                    if (obj.Title == "Portal Simples")
                    {
                        //ABRE NOVAMENTE PAGINA DO SALESFORCE
                        if (SALESFORCE == 0)
                        {
                            obj.ExecuteScript("window.open('https://simples.bancointer.com.br/IdPServlet?idp_id=14nkilu7bh8bi','_blank');");
                        }


                        //ABRE NOVAMENTE PAGINA DO CRM
                        if (INTERCON == 0)
                        {
                            obj.ExecuteScript("window.open('https://app.intercom.com/admins/sign_in','_blank');");
                        }

                        break;
                    }

                }

                //FIM DE PROCESSO
                return 0;

            }
            catch (Exception err)
            {
                MessageBox.Show("Erro: " + err.Message);
                return 1;
            }
        }

        /// <summary>
        /// VerificaPaginaAbertas
        /// </summary>
        /// <param name="sLogin"></param>
        /// <param name="sSenha"></param>
        public int VerificaPaginaAbertas_Debug()
        {
            try
            {
                DateTime dt;
                Boolean Sucess = false;
                string Titulo = "";

                //Limpa variável de verificação
                PortalSimples = 0;
                TESTE1 = 0;

                try
                {
                    //VERIFICA SE POSSUE ALGUMA JANELA ABERTA
                    if (obj.WindowHandles.Count == 0)
                    {
                        //NENHUMA PAGINA ABERTA ABRE PORTAL SIMPLES
                        return 3;
                    }
                    else
                    {
                        //PRIMEIRA JENELA
                        obj.SwitchTo().Window(obj.WindowHandles[0]);
                    }
                }
                catch (Exception)
                {
                    //ERRO
                    return 1;
                }


                //EFETUA LEITURA DE JANELA ABERTA
                for (int x = 0; x < obj.WindowHandles.Count; x++)
                {
                    obj.SwitchTo().Window(obj.WindowHandles[x]);

                    Titulo = obj.Title.ToString();

                    //VERIFICA SISTES ABERTOS
                    if (obj.Title == "Portal Simples")
                    {
                        PortalSimples = 1;

                    }


                    if (Titulo.ToUpper().IndexOf(("Google").ToUpper()) > -1)
                    {
                        TESTE1 = 1;
                    }

                    Application.DoEvents();
                }

                //ABRE JANELA FECHADAS
                if (PortalSimples == 0)
                {

                    try
                    {

                        Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");

                        foreach (var chromeDriverProcess in chromeDriverProcesses)
                        {
                            chromeDriverProcess.Kill();
                        }

                        //KILL
                        Process oNice = Process.Start(@"c:\\temp\\kill_chrome.bat");

                    }
                    catch (Exception)
                    {

                    }

                    //ABRE PORTAL SIMPLES PARA LOGAR
                    return 3;
                }


                //ABRE NOVAMENTE PAGINA DO TESTE
                if (TESTE1 == 0)
                {

                    obj.ExecuteScript("window.open('https://www.google.com.br/','_blank');");

                    Thread.Sleep(250);

                    for (int xxx = 0; xxx < obj.WindowHandles.Count; xxx++)
                    {
                        obj.SwitchTo().Window(obj.WindowHandles[xxx]);

                        Titulo = obj.Title.ToString();

                        if (Titulo.ToUpper().IndexOf(("Google").ToUpper()) > -1)
                        {
                            obj.Navigate().GoToUrl("https://m.facebook.com/");
                        }

                    }

                    Thread.Sleep(250);

                }


                if (TESTE1 == 0)
                {
                    obj.ExecuteScript("window.open('https://www.google.com.br/','_blank');");
                }

                //FIM DE PROCESSO
                return 0;

            }
            catch (Exception err)
            {
                MessageBox.Show("Erro: " + err.Message);
                return 1;
            }
        }


        /// <summary>
        /// VerificaPaginaAbertas
        /// </summary>
        /// <param name="sLogin"></param>
        /// <param name="sSenha"></param>
        public int AbrePaginas(int Logado)
        {
            try
            {
                DateTime dt;
                Boolean Sucess = false;
                Boolean Aguarde_Text = false;
                string Titulo = "";


                //FECHA TODOS OS SITES
                if (Logado == 0)
                {
                    //MATA PROCESSOS CHROMEDRIVE
                    Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");

                    foreach (var chromeDriverProcess in chromeDriverProcesses)
                    {
                        chromeDriverProcess.Kill();
                    }

                    //KILL
                    Process oNice = Process.Start(@"c:\\temp\\kill_chrome.bat");

                    return 0;
                }

                try
                {
                    //VERIFICA SE POSSUE ALGUMA JANELA ABERTA
                    if (obj.WindowHandles.Count == 0)
                    {
                        //NENHUMA PAGINA ABERTA ABRE PORTAL SIMPLES
                        return 3;
                    }
                    else
                    {
                        //PRIMEIRA JENELA
                        obj.SwitchTo().Window(obj.WindowHandles[0]);
                    }
                }
                catch (Exception)
                {
                    //ERRO
                    return 1;
                }



                //EFETUA LEITURA DE JANELA ABERTA
                for (int x = 0; x < obj.WindowHandles.Count; x++)
                {
                    obj.SwitchTo().Window(obj.WindowHandles[x]);

                    //VERIFICA SISTES ABERTOS
                    if (obj.Title == "Portal Simples")
                    {

                        string href = "";

                        //Abre pagina Genesys
                        foreach (var item in obj.FindElements(By.TagName("div")))
                        {
                            if (item.GetAttribute("Title") == "Genesys Cloud")
                            {
                                foreach (var item2 in item.FindElements(By.TagName("a")))
                                {

                                    href = item2.GetAttribute("href").ToString();

                                    if (href.ToUpper().IndexOf(("https://simples.bancointer.com.br/IdPServlet").ToUpper()) > -1)
                                    {
                                        IJavaScriptExecutor executor = (IJavaScriptExecutor)obj;
                                        executor.ExecuteScript("arguments[0].click();", item2);
                                        break;
                                    }

                                }
                            }
                        }

                        Thread.Sleep(250);

                        //Atualiza pagina Genesys
                        for (int xx = 0; xx < obj.WindowHandles.Count; xx++)
                        {
                            obj.SwitchTo().Window(obj.WindowHandles[xx]);

                            Aguarde_Text = false;

                            //VERIFICA SE ESTÁ NA PAGINA PRINCIPAL
                            while (Aguarde_Text == false)
                            {
                                Aguarde_Text = Wait_Text("Collaborate/ Communicate");

                                //SE NÃO IDENTIFICAR PAGINA DEFAULT - REALIZA NOVAMENTE
                                if (Aguarde_Text == true)
                                {
                                    Thread.Sleep(250);
                                }

                            }

                            Titulo = obj.Title.ToString();

                            if (Titulo.ToUpper().IndexOf(("BancoInter").ToUpper()) > -1 || Titulo.ToUpper().IndexOf(("Genesys").ToUpper()) > -1 || Titulo.ToUpper().IndexOf(("FLX").ToUpper()) > -1)
                            {
                                obj.Navigate().GoToUrl("https://apps.mypurecloud.com/directory");
                            }

                        }

                        Thread.Sleep(250);

                        obj.SwitchTo().Window(obj.WindowHandles[x]);

                        //Abre pagina Intercom
                        foreach (var item in obj.FindElements(By.TagName("div")))
                        {
                            if (item.GetAttribute("Title") == "Intercom")
                            {
                                foreach (var item2 in item.FindElements(By.TagName("a")))
                                {

                                    href = item2.GetAttribute("href").ToString();

                                    if (href.ToUpper().IndexOf(("https://simples.bancointer.com.br/IdPServlet").ToUpper()) > -1)
                                    {
                                        IJavaScriptExecutor executor = (IJavaScriptExecutor)obj;
                                        executor.ExecuteScript("arguments[0].click();", item2);
                                        break;
                                    }

                                }
                            }
                        }

                        Thread.Sleep(250);

                        //Abre pagina Salesforce
                        foreach (var item in obj.FindElements(By.TagName("div")))
                        {
                            if (item.GetAttribute("Title") == "Salesforce")
                            {
                                foreach (var item2 in item.FindElements(By.TagName("a")))
                                {

                                    href = item2.GetAttribute("href").ToString();

                                    if (href.ToUpper().IndexOf(("https://simples.bancointer.com.br/IdPServlet").ToUpper()) > -1)
                                    {
                                        IJavaScriptExecutor executor = (IJavaScriptExecutor)obj;
                                        executor.ExecuteScript("arguments[0].click();", item2);
                                        break;
                                    }

                                }
                            }
                        }

                    }


                    Application.DoEvents();
                }


                //FIM DE PROCESSO
                return 0;

            }
            catch (Exception err)
            {
                MessageBox.Show("Erro: " + err.Message);
                return 1;
            }
        }


        public Boolean Wait_Text(string valor)
        {
            DateTime dt;
            bool exec = false;
            int sec = 0;
            string sbody = "";

            try
            {
                dt = DateTime.Now;

                obj.SwitchTo().DefaultContent();

                //TEXTO DO MODAL 
                sbody = obj.FindElement(By.TagName("body")).GetAttribute("innerText");

                while (exec == false)
                {

                    //VERIFICA TEXTO NA TELA SE CARREGOU
                    if (sbody.IndexOf(valor) > 0)
                    {
                        exec = true;
                    }
                    else
                    {

                        sec = Convert.ToInt32((DateTime.Now - dt).TotalSeconds);

                        //time out
                        if (sec > 3)
                        {
                            obj.SwitchTo().DefaultContent();
                            exec = true;
                        }

                    }

                }
                return exec;

            }
            catch (Exception)
            {
                return exec;
            }


        }


        /// <summary>
        /// WaitObject
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="stipo"></param>
        /// <returns></returns>
        public bool WaitObject(string valor, string stipo)
        {

            try
            {
                DateTime dt;
                bool exec = true;
                List<IWebElement> conta;
                bool bok = false;
                int sec = 0;

                dt = DateTime.Now;

                while (exec == true)
                {
                    Application.DoEvents();

                    conta = new List<IWebElement>();

                    //tipo
                    if (stipo.ToLower() == "id")
                    {
                        conta.AddRange(obj.FindElements(By.Id(valor)));
                    }

                    if (stipo.ToLower() == "name")
                    {
                        conta.AddRange(obj.FindElements(By.Name(valor)));
                    }

                    if (stipo.ToLower() == "xpath")
                    {
                        conta.AddRange(obj.FindElements(By.XPath(valor)));
                    }

                    if (stipo.ToLower() == "CssSelector")
                    {
                        conta.AddRange(obj.FindElements(By.CssSelector(valor)));
                    }


                    if (conta.Count > 0)
                    {
                        bok = true;
                        exec = false;
                    }
                    else
                    {

                        sec = Convert.ToInt32((DateTime.Now - dt).TotalSeconds);


                        //time out
                        if (sec > 25)
                        {
                            bok = false;
                            exec = false;
                        }

                    }

                    conta = null;

                    Thread.Sleep(250);
                    Application.DoEvents();
                }


                return bok;


            }
            catch (Exception)
            {
                return false;
            }



        }


    }
}
