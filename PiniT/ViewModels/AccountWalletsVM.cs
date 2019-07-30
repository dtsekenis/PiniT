using PiniT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PiniT.ViewModels
{

    public class AddCreditsAccountWalletVM
    {
        public AccountWallet Wallet { get; set; }

        [DisplayName("Add Credits")]
        [Range(0,500)]
        public decimal AmountToBeAdded { get; set; }
    }
}