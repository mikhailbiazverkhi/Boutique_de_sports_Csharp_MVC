namespace TP_test.Configurations
{
    public class SmtpConfig
    {
        public int Port { get; set; }
        public string Serveur { get; set; } = string.Empty;
        public string Utilisateur { get; set; } = string.Empty;
        public string MotDePasse { get; set; } = string.Empty;
    }
}
