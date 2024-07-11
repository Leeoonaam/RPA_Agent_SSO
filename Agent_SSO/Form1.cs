using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using OpenQA.Selenium.Chrome;
using System.Reflection;
using RestSharp;
using Newtonsoft;
using Newtonsoft.Json;

namespace Agent_SSO
{
    public partial class frmIndex : Form
    {

        public string sIdUser = "";
        public string sNomeUser = "";
        public string sVersao = "";
        public string sIdRamal = "";
        public string sIdEquipe = "";
        public string sIdPerfil = "";
        public string sLoginPortalSimples = "";
        public string sSenhaPortalSimples = "";
        public string sLoginCRM = "";
        public string sSenhaCRM = "";
        public string sLoginCIM = "";
        public string sSenhaCIM = "";
        public string sLoginSKORE = "";
        public string sSenhaSKORE = "";
        public string sLoginINTERCON = "";
        public string sSenhaINTERCON = "";
        public string sLoginGENESYS = "";
        public string sSenhaGENESYS = "";
        public string sLoginSalesForce = "";
        public string sSenhaSalesForce = "";


        /// <summary>
        /// frmIndex
        /// </summary>
        public frmIndex()
        {
            InitializeComponent();
        }

        // instancia class de automação
        Automation atm = new Automation();
        
        /// <summary>
        /// frmIndex_Resize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmIndex_Resize(object sender, EventArgs e)
        {
            try
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    ShowIcon = false;
                    notifyIcon1.Visible = true;
                    notifyIcon1.ShowBalloonTip(1000);
                }
            }
            catch (Exception err)
            {
                lblstatus.Text  = "Erro: " + err.Message;
                return;
            }
        }


        /// <summary>
        /// notifyIcon1_MouseDoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                ShowInTaskbar = true;
                notifyIcon1.Visible = false;
                WindowState = FormWindowState.Normal;
            }
            catch (Exception err)
            {
                lblstatus.Text  = "Erro: " + err.Message;
                return;
            }
        }

        /// <summary>
        /// AtualizaStatusSSO
        /// </summary>
        /// <param name="sStatus"></param>
        public void AtualizaStatusSSO(string sStatus)
        {
            try
            {
                lblstatus.Text = sStatus;

                Application.DoEvents();
            }
            catch (Exception err)
            {
               lblstatus.Text  = "Erro: " + err.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sIdUser"></param>
        /// <param name="sIdRamal"></param>
        /// <param name="IdCodLegado"></param>
        private void ArmazenaHistoricoLoginLegados(string sIdUser, int iIdCodLegado, string sDataAcesso)
        {
            try
            {
                int iResultProc = 0;
                gergatlink ogat = new gergatlink();

                using (SqlConnection cn2 = ogat.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn2;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "GravaLogSSO";

                        cmd.Parameters.AddWithValue("@IdUser", sIdUser);
                        cmd.Parameters.AddWithValue("@IdSistemaLegado", iIdCodLegado);
                        cmd.Parameters.AddWithValue("@DataIniProcess", sDataAcesso);

                        iResultProc = cmd.ExecuteNonQuery();

                        if (iResultProc > 0)
                        {
                            // sucesso
                        }
                        else
                        {
                            MessageBox.Show("Erro: O Log não foi inserido na base...");
                            return;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Erro: " + err.Message);
                return;
            }
        }

        
        /// <summary>
        /// frmIndex_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmIndex_Load(object sender, EventArgs e)
        {
            try
            {

                //Desativa Timer
                timer1.Enabled = false;

                //pega versao app
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                sVersao = fvi.FileVersion;

                AtualizaStatusSSO("Validando Versão...");

                if (ValidaExpireDate() == "Expirado")
                {
                    MessageBox.Show("Erro: O GAT Link SSO Expirou...", "GAT Link - SSO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Application.Exit();
                }

                AtualizaStatusSSO("Abrindo SSO - GAT Link");

                if (VerificaLoginUsuario(System.Windows.Forms.SystemInformation.UserName) == "Sair")
                {
                    return;
                }

                // pega o nome do usuario logado na maquina
                lblLogin.Text = System.Windows.Forms.SystemInformation.UserName;


                //Loga o usuario
                AtualizaStatusUsuario();

                // cria icone systray
                notifyIcon1.BalloonTipText = "Aplicativo Minimizado";
                notifyIcon1.BalloonTipTitle = "GAT Link - SSO";


            }
            catch (Exception err)
            {
                AtualizaStatusSSO("Erro: " + err.Message);
                return;
            }
        }

        /// <summary>
        /// GetAllowMakeCall
        /// </summary>
        /// <param name="scodGP"></param>
        /// <returns></returns>
        public RetornoAPICode7 GetAllowMakeCall(string scodGP)
        {
            try
            {

                var client = new RestClient("http://172.28.2.93:8181/Ayty/App/AytyHumanResourcesWebApi/Api/TimeSheet/GetAllowMakeCall/" + scodGP);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                IRestResponse response = client.Execute(request);

                string sret = response.Content;
                sret = sret.Replace(@"""{", "{").Replace(@"}""", "}").Replace(@"\", "");


                RetornoAPICode7 myDeserializedClass = JsonConvert.DeserializeObject<RetornoAPICode7>(sret);

                client = null;

                return myDeserializedClass;

            }
            catch (Exception err)
            {

                string serr = err.Message;

                return null;
            }
        }


        /// <summary>
        /// ValidaExpireDate
        /// </summary>
        /// <returns></returns>
        public string ValidaExpireDate()
        {
            try
            {
                int iDataAtual = 0;
                DateTime dData = DateTime.Now;
                string sData = "";
                int iDataExpire = 20211215;

                gergatlink ogat = new gergatlink();

                using (SqlConnection cn = ogat.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "select getdate() as Data";

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            dData = Convert.ToDateTime(dr["Data"].ToString());
                        }

                        dr.Close();
                    }
                }
                
                sData = dData.ToString("yyyyMMdd");

                iDataAtual = Convert.ToInt32(sData);

                if (iDataExpire <= iDataAtual)
                {
                    return "Expirado";
                }

                return "OK";
            }
            catch (Exception err)
            {
                return err.Message;
            }
        }

        /// <summary>
        /// VerificaLoginUsuario
        /// </summary>
        /// <param name="sLogin"></param>
        private string VerificaLoginUsuario(string sLogin)
        {
            try
            {
                
                AtualizaStatusSSO("Verificando os dados do usuário...");

                gergatlink ogat = new gergatlink();
                
                string ssql = @"select	IdCodUsuario,Nome,SVERSAO
                                from	GATUsuario (nolock)
                                where	login = '" + sLogin + "' and habilitado = 1";

                using (SqlConnection cn = ogat.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = ssql;

                        SqlDataReader dr = cmd.ExecuteReader();
                        
                        if (dr.Read())
                        {

                            AtualizaStatusSSO("Carregando os dados do usuário...");

                            sIdUser = dr["IdCodUsuario"].ToString();
                            sNomeUser = dr["Nome"].ToString();
                            //sVersarUser = dr["SVERSAO"].ToString();

                        }
                        else
                        {
                            MessageBox.Show("Erro: Login não encontrado...", "GAT Link - SSO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Application.Exit();
                            return "Sair";
                        }

                        // COLOCA O DO USUARIO
                        //lblNome.Text = sNomeUser;

                        // coloca o numero da versão
                        //lblVersao.Text = sVersarUser;

                        AtualizaStatusSSO("SSO Aberto com sucesso...");

                        dr.Close();

                    }
                }
                return "";
            }
            catch (Exception err)
            {
                MessageBox.Show("Erro: " + err.Message);
                Application.Exit();
                return "";
            }
        }

        /// <summary>
        /// PegaDataHoraAtual
        /// </summary>
        public string PegaDataHoraAtual()
        {
            try
            {
                gergatlink ogat = new gergatlink();
                string sData = "";
                DateTime dData;

                using (SqlConnection cn = ogat.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "select getdate() as data";

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            dData = Convert.ToDateTime(dr["data"].ToString());
                            sData = dData.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        dr.Close();
                    }
                }

                return sData;
            }
            catch (Exception err)
            {
                MessageBox.Show("Erro: " + err.Message);
                return "";
            }
        }



        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PortalSimples_Automacao()
        {
            try
            {

                int Status_OP = 0;

                if (OptLogado.Checked == true)
                {
                    Status_OP = 1;
                }
                else
                {
                    Status_OP = 0;
                }

                gergatlink ogat = new gergatlink();

                using (SqlConnection cn = ogat.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"select	LOGIN_PORTAL_SIMPLES,
		                                            SENHA_PORTAL_SIMPLES
                                            from	GATUsuario (nolock)
                                            where	IDCodUsuario = " + sIdUser;

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            sLoginPortalSimples = dr["LOGIN_PORTAL_SIMPLES"].ToString();
                            sSenhaPortalSimples = dr["SENHA_PORTAL_SIMPLES"].ToString();
                            sSenhaPortalSimples = Descrypt.DecryptData(sSenhaPortalSimples);
                        }
                        else
                        {
                            AtualizaStatusSSO("Usuario ou Senha não cadastrado!!!");
                            MessageBox.Show("Erro: Usuário não encontrado...", "GAT Link - Portal Simples", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        dr.Close();
                    }
                }

                btnPortalSimples.Enabled = false;


                if (sLoginPortalSimples != "" && sSenhaPortalSimples != "")
                {
                    AtualizaStatusSSO("Abrindo o Portal Simples");

                    string sData = PegaDataHoraAtual();

                    this.WindowState = FormWindowState.Minimized;

                    //ABRE PORTAL SIMPLES
                    if (atm.RealizaLoginPortalSimples(sLoginPortalSimples, sSenhaPortalSimples, Status_OP) == 1)
                    {
                        this.WindowState = FormWindowState.Normal;
                        MessageBox.Show("Erro: O site não carregou corretamente...", "GAT Link - Portal Simples", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.WindowState = FormWindowState.Normal;

                    ArmazenaHistoricoLoginLegados(sIdUser, 7, sData);

                    AtualizaStatusSSO("Portal Simples Aberto com sucesso - Abre Demais Paginas");

                    //ABRE DEMAIS PAGINAS 
                    if (atm.AbrePaginas(Status_OP) == 1)
                    {
                        this.WindowState = FormWindowState.Normal;
                        MessageBox.Show("Erro: O site não carregou corretamente...", "GAT Link - Portal Simples", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    AtualizaStatusSSO("Paginas Abertas com sucesso");

                    timer1.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Usuario ou Senha não cadastrado!!!");
                    AtualizaStatusSSO("Usuario ou Senha não cadastrado!!!");
                }

                btnPortalSimples.Enabled = true;

            }
            catch (Exception err)
            {
                MessageBox.Show("Erro: [FUNÇÃO PORTAL SIMPLES] " + err.Message);
                btnPortalSimples.Enabled = true;
                return;
            }
        }


        /// <summary>
        /// btnCrm_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPortalSimples_Click(object sender, EventArgs e)
        {
            try
            {
                int Status_OP = 0;

                if (OptLogado.Checked == true)
                {
                    Status_OP = 1;
                }
                else
                {
                    Status_OP = 0;
                }

                gergatlink ogat = new gergatlink();

                using (SqlConnection cn = ogat.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"select	LOGIN_PORTAL_SIMPLES,
		                                            SENHA_PORTAL_SIMPLES
                                            from	GATUsuario (nolock)
                                            where	IDCodUsuario = " + sIdUser;

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            sLoginPortalSimples = dr["LOGIN_PORTAL_SIMPLES"].ToString();
                            sSenhaPortalSimples = dr["SENHA_PORTAL_SIMPLES"].ToString();
                            sSenhaPortalSimples = Descrypt.DecryptData(sSenhaPortalSimples);
                        }
                        else
                        {
                            AtualizaStatusSSO("Usuario ou Senha não cadastrado!!!");
                            MessageBox.Show("Erro: Usuário não encontrado...", "GAT Link - Portal Simples", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        dr.Close();
                    }
                }

                btnPortalSimples.Enabled = false;


                if (sLoginPortalSimples != "" && sSenhaPortalSimples != "")
                {
                    AtualizaStatusSSO("Abrindo o Portal Simples");

                    string sData = PegaDataHoraAtual();
                    
                    this.WindowState = FormWindowState.Minimized;

                    //ABRE PORTAL SIMPLES
                    if (atm.RealizaLoginPortalSimples(sLoginPortalSimples, sSenhaPortalSimples, Status_OP) == 1)
                    {
                        this.WindowState = FormWindowState.Normal;
                        MessageBox.Show("Erro: O site não carregou corretamente...", "GAT Link - Portal Simples", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.WindowState = FormWindowState.Normal;

                    ArmazenaHistoricoLoginLegados(sIdUser, 7, sData);

                    AtualizaStatusSSO("Portal Simples Aberto com sucesso - Abre Demais Paginas");

                    //ABRE DEMAIS PAGINAS 
                    if (atm.AbrePaginas(Status_OP) == 1)
                    {
                        this.WindowState = FormWindowState.Normal;
                        MessageBox.Show("Erro: O site não carregou corretamente...", "GAT Link - Demais Paginas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    AtualizaStatusSSO("Paginas Abertas com sucesso");
                    
                    
                    timer1.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Usuario ou Senha não cadastrado!!!");
                    AtualizaStatusSSO("Usuario ou Senha não cadastrado!!!");
                }

                btnPortalSimples.Enabled = true;
               
            }
            catch (Exception err)
            {
                MessageBox.Show("Erro: [btnPortalSimples_Click] " + err.Message);
                btnPortalSimples.Enabled = true;
                return;
            }
        }


        /// <summary>
        /// AtualizaStatusUsuario
        /// </summary>
        public void AtualizaStatusUsuario()
        {
            try
            {
                gergatlink ogat = new gergatlink();

                using (SqlConnection cn = ogat.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update gatusuario set dataultstatus = getdate(), codstatususer = 1, SVERSAO='" + sVersao + "' where idcodusuario = " + sIdUser;

                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Erro: " + err.Message);
            }
        }


       

        /// <summary>
        /// frmIndex_FormClosing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmIndex_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                gergatlink ogat = new gergatlink();

                if (sIdUser == "")
                {
                    return;
                }

                using (SqlConnection cn = ogat.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update gatusuario set dataultstatus = getdate(), codstatususer = 0, emuso=0 where idcodusuario = " + sIdUser;

                        cmd.ExecuteNonQuery();
                    }
                }

                atm.FechaChromeDriver();
            }
            catch (Exception err)
            {
                MessageBox.Show("Erro: [frmIndex_FormClosing] " + err.Message);
            }
        }

        


        //timer1_Tick
        private void timer1_Tick(object sender, EventArgs e)
        {

            int Status_OP = 0;

            if (OptLogado.Checked == true)
            {
                Status_OP = 1;
            }
            else
            {
                Status_OP = 0;
            }
            
            timer1.Enabled = false;


            //ABRE PORTAL SIMPLES
            if (atm.VerificaPaginaAbertas(Status_OP) == 3)
            {
                PortalSimples_Automacao();
            }

            timer1.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //ABRE PORTAL SIMPLES
            if (atm.RealizaLoginPortalSimples_Debug("FO006599", "SSO2021*") == 1)
            {
                this.WindowState = FormWindowState.Normal;
                MessageBox.Show("Erro: O site não carregou corretamente...", "GAT Link - Portal Simples", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            //ABRE PORTAL SIMPLES
            if (atm.VerificaPaginaAbertas_Debug() == 3)
            {
                //ABRE PORTAL SIMPLES
                if (atm.RealizaLoginPortalSimples_Debug("FO006599", "SSO2021*") == 1)
                {
                    this.WindowState = FormWindowState.Normal;
                    MessageBox.Show("Erro: O site não carregou corretamente...", "GAT Link - Portal Simples", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
                    

        }

        private void btnPortal_Simples_Click(object sender, EventArgs e)
        {
            try
            {

                int Status_OP = 0;

                if (OptLogado.Checked == true)
                {
                    Status_OP = 1;
                }
                else
                {
                    Status_OP = 0;
                }

                gergatlink ogat = new gergatlink();

                using (SqlConnection cn = ogat.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"select	LOGIN_PORTAL_SIMPLES,
		                                            SENHA_PORTAL_SIMPLES
                                            from	GATUsuario (nolock)
                                            where	IDCodUsuario = " + sIdUser;

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            sLoginPortalSimples = dr["LOGIN_PORTAL_SIMPLES"].ToString();
                            sSenhaPortalSimples = dr["SENHA_PORTAL_SIMPLES"].ToString();
                            sSenhaPortalSimples = Descrypt.DecryptData(sSenhaPortalSimples);
                        }
                        else
                        {
                            AtualizaStatusSSO("Usuario ou Senha não cadastrado!!!");
                            MessageBox.Show("Erro: Usuário não encontrado...", "GAT Link - Portal Simples", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        dr.Close();
                    }
                }

                btnPortalSimples.Enabled = false;


                if (sLoginPortalSimples != "" && sSenhaPortalSimples != "")
                {
                    AtualizaStatusSSO("Abrindo o Portal Simples");

                    string sData = PegaDataHoraAtual();

                    this.WindowState = FormWindowState.Minimized;

                    //ABRE PORTAL SIMPLES
                    if (atm.RealizaLoginPortalSimples(sLoginPortalSimples, sSenhaPortalSimples, Status_OP) == 1)
                    {
                        this.WindowState = FormWindowState.Normal;
                        MessageBox.Show("Erro: O site não carregou corretamente...", "GAT Link - Portal Simples", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.WindowState = FormWindowState.Normal;

                    ArmazenaHistoricoLoginLegados(sIdUser, 7, sData);

                    AtualizaStatusSSO("Portal Simples Concluido");

                    // timer1.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Usuario ou Senha não cadastrado!!!");
                    AtualizaStatusSSO("Usuario ou Senha não cadastrado!!!");
                }

                btnPortalSimples.Enabled = true;

            }
            catch (Exception err)
            {
                MessageBox.Show("Erro: [btnPortalSimples_Click] " + err.Message);
                btnPortalSimples.Enabled = true;
                return;
            }
        }

        private void btnOutros_Sites_Click(object sender, EventArgs e)
        {
            int Status_OP = 0;

            if (OptLogado.Checked == true)
            {
                Status_OP = 1;
            }
            else
            {
                Status_OP = 0;
            }

            //Abre outros sites
            AtualizaStatusSSO("Abrindo outros sites");

            //ABRE DEMAIS PAGINAS 
            if (atm.AbrePaginas(Status_OP) == 1)
            {
                this.WindowState = FormWindowState.Normal;
                MessageBox.Show("Erro: O site não carregou corretamente...", "GAT Link - Demais Paginas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //Abre outros sites
            AtualizaStatusSSO("Sites Abertos com sucesso!");

        }

        private void btnVerifica_sites_Click(object sender, EventArgs e)
        {
            int Status_OP = 0;

            if (OptLogado.Checked == true)
            {
                Status_OP = 1;
            }
            else
            {
                Status_OP = 0;
            }

            //Abre outros sites
            AtualizaStatusSSO("Inicio de verificação");

            //ABRE PORTAL SIMPLES
            if (atm.VerificaPaginaAbertas(Status_OP) == 1)
            {
                this.WindowState = FormWindowState.Normal;
                MessageBox.Show("Erro: O site não carregou corretamente...", "GAT Link - Demais Paginas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //Abre outros sites
            AtualizaStatusSSO("Vericação realizada com sucesso!");

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        
    }
}
