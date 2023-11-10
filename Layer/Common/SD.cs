using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum TransactionType
    {

        [Description("Initial Stock")]
        InitialStock = 10,
        [Description("Purchase")]
        Purchase = 20,
        [Description("Sales")]
        Sales = 30,
        [Description("Stock Adjusment")]
        Adjustment = 40,
        [Description("Stock Transfer")]
        Transfer = 50,
        [Description("Purchase Return")]
        Purchase_Return = 25,
        [Description("Sales Return")]
        Sales_Return = 35,

    }
    public static class SD
    {

        public static class Message
        {
            public const string SaveSuccess = "Data Is Saved";
        }

        public static class Register
        {
            public const string UserFound = "User Name telah ada";
            public const string RegisterSuccess = "Pendaftaran berhasil";
            public const string SiginSuccess = "SignIn Berhasil";
            public const string SiginFailed = "Password atau UserName salah";
            public const string SignUpModelNotValid = "Input Email atau Phone tidak valid";
            public const string DataDuplikat = "Data tidak bisa disimpan karena data sudah ada.";
            public const string DataDisimpan = "Data telah disimpan .";
            public const string DataTidakDiplih = "Pilih data dahulu.";
            public const string DataSaveNull = "Tidak ada record yang bisa disimpan. Null values !";

        }
    }
}
