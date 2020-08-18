using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace stockstock_quote_alert {
    internal class Correio {
        #region Campos
        private MailMessage Mail { get; set; }
        #endregion
        #region Constructor
        internal Correio() {
            Mail = new MailMessage();
        }
        #endregion
    }
}
