using System.ComponentModel;

namespace LMSBackOfficeAPI.Enum
{
    public static class Enums
    {
        public enum HttpMethod
        {
            Get,
            Post,
            Put,
            Delete
        }

        public enum FileUploadType
        {
            [Description("Photos")]
            Photos = 1,
            [Description("Passport")]
            Passport,
            [Description("Videos")]
            Videos,
            [Description("Driving license")]
            Driving_license,
            [Description("NIC")]
            NIC,
            [Description("Address proof")]
            Address_proof,
            [Description("Ticket")]
            Ticket,
            [Description("DepositReceipt")]
            DepositReceipt,
            [Description("LoginBackground")]
            LoginBackground,
            [Description("Temp Upload")]
            TempUpload,
            [Description("Other")]
            Other,
        }

        public enum Secquence_For
        {
            ACC_LOGIN = 1,
            CUS_ID,
            ACC_LOGIN_IB_AND_STAFF
        }

        public enum UniqueCodeFor
        {
            login = 1,
            customers,
            kyc_crm,
            pipeline,
            leads,
            kyc_setting,
            deposit_WL,
            deposit_TA,
            transfer_WL,
            transfer_TA,
            withdraw_WL,
            withdraw_TA,
            virtualWallet,
            kyc_Answer,
            kyc_Question,
            reference_code,
            tickets,
            ticket_attachment,
            withdrawNodes_Status,
            crmroletype_code,
            deposit_config,
            menu,
            page,
            operation,
            loginbackground,
            staff
        }

        public enum Account_Status
        {
            Pending = 100,
            ToOpen = 110,
            Opened = 111,
            Enabled = 112,
            Disabled = 101,
            ReadOnly = 102,
            Cancelled = 103
        }

        public enum Verification_Status
        {
            [Description("Not Required")]
            NotRequired,

            [Description("Pending")]
            Pending,

            [Description("In-Progress")]
            InProgress,

            [Description("Completed")]
            Completed,
        }

        public enum Customer_Status
        {
            [Description("Not Required")]
            NotRequired,

            [Description("Registered")]
            Registered,

            [Description("Pending")]
            Pending,

            [Description("Verified")]
            Verified,

            [Description("Rejected")]
            Rejected,

            [Description("Black Listed")]
            BlackListed
        }

        public enum KYC_Status
        {
            [Description("Not Required")]
            NotRequired,

            [Description("Pending")]
            Pending,

            [Description("In-Progress")]
            InProgress,

            [Description("Completed")]
            Completed,
        }

        public enum Customer_Is_OpenAccountStatus
        {
            Pending = 1,
            ToOpen,
            Inprogress,
            Opened,
            Rejected,
            Blocked
        }

        public enum Business_Type
        {
            [Description("ECN")]
            BCECN_0001_0322,
            [Description("Standard")]
            BCSTD_0002_0322,
            [Description("Islamic Standard")]
            BCISTD_0003_0322,
            [Description("Basic")]
            BCBSC_0004_0322,
            [Description("Professional")]
            BCPRO_0005_0322,
            [Description("Demo")]
            BCDEMO_0006_0222
        }

        public enum EmailEventType
        {
            PasswordReset,
            EmailChange,
            AddBankCard,
            Registration,
            TradingAccount,
            IBAccount,
            Deposit,
            Withdraw,
            Transfer,
            TradeInformation,
            CommissionPosting,
            FinancialDeductions,
            AccountBlock,
            AccountUnblock,
            UserBlocklisted,
            ServiceTicketResolved,
            CustomerProfileUpdate,
            CustomerTradingAccountOpening,
            RegistrationSendOTP,
            RegistrationSuccess,
            EmailChangeSendOTP,
            EmailChangedSuccess,
            TradingAccountPasswordChange,
            TradingAccountLeverageChange,
            TwoWayAuthSendOTP,
            ForgetPasswordSendOTP,
            PasswordChangeSuccess,
            CustomerStatusVerified,
            CustomerStatusRejected,
            AffiliateStatusVerified,
            AffiliateStatusRejected,
            CustomNotification,
            TicketStatusChanged,
            TradingAccountUserStatusChange,
            CRMRegistrationSuccess,
            SetNewPasswordOTP,
            ARegistrationSuccess,
            CRegistrationSuccess,
            FundPasswordChangeSuccess,
            ResetfundPassword,
            ResetFundPasswordOTP
        }

        public enum EmailStatus
        {
            [Description("To Send")]
            ToSend = 111,

            [Description("Sent")]
            Sent = 100,

            [Description("In-Progress")]
            InProgress = 113
        }

        public enum User_Type
        {
            Admin,
            Manager,
            CRM,
            Customer,
            IB,
            Staff
        }

        public enum TicketStatus
        {
            [Description("Open")]
            Open,

            [Description("In Process")]
            Inprocess,

            [Description("Resolved")]
            Resolved,
        }
        public enum PaymentMethod
        {
            [Description("Wire Transfer")]
            WireTransfer=1,

            [Description("Alternative Method")]
            AlternativeMethod,

            [Description("Crypto Wallet")]
            CryptoWallet,
        }
        public enum PaymentSource
        {
            [Description("Trading Account")]
            TradingAccount,

            [Description("Wallet")]
            Wallet,
        }

        public enum TransferStatus
        {
            [Description("In Process")]
            Transaction_Inprocess = 100,

            [Description("Wrong Signature")]
            Wrong_Signature = 108,

            [Description("Not enough balance")]
            Not_enough_balance = 105,

            [Description("From account can not be found")]
            Transfer_From_account_can_not_be_found = 101,

            [Description("To account can not be found")]
            Transfer_To_account_can_not_be_found = 103,

            [Description("Illegal out amount")]
            Transfer_Illegal_out_amount = 110,

            [Description("Not enough margin")]
            Transfer_Not_enough_margin = 106,

            [Description("Type not allowed ")]
            Transfer_Type_not_allowed = 109,

            [Description("Success")]
            Transfer_Success = 111,

            [Description("Rejected")]
            Rejected = 112,
        }
        public enum TradescmdStatus
        {
            [Description("Buy")]
            buy = 0, 
            [Description("Sell")]
            sell = 1,
            [Description("Balance")]
            balance = 6,
            [Description("Credit")]
            credit = 7
        }
        public enum DepositStatus
        {
            [Description("To Deposit Alternative Method")]
            To_DepositByAlternativeMethod = 97,
            [Description("To Deposit")]
            To_Deposit = 98,
            [Description("In Process")]
            Transaction_Inprocess = 100,

            [Description("Paid")]
            Deposit_Paid = 200,
            [Description("Wrong Amount")]
            Deposit_Wrong_Amount = 104,

            [Description("Success")]
            Deposit_Success = 300,

            [Description("Account can not be found")]
            Deposit_Account_can_not_be_found = 107,

            [Description("Wrong Signature")]
            Wrong_Signature = 108,


            [Description("Not enough balance")]
            Not_enough_balance = 105,

            [Description("Rejected")]
            Rejected = 112,

        }

        public enum WithdrawStatus
        {
            [Description("Withdrawal Requested")]
            WithdrawalRequested = 97,
            [Description("To Withdraw")]
            To_Withdraw = 100,

            [Description("Rejected")]
            Rejected = 1001,

            [Description("CS Approval Pending")]
            CS_ApprovalPending = 102,

            [Description("CS Approved")]
            CS_Approved = 103,

            [Description("CS Rejected")]
            CS_Rejected = 1031,

            [Description("OP Approval Pending")]
            OP_ApprovalPending = 104,

            [Description("OP Approved")]
            OP_Approved = 105,

            [Description("OP Rejected")]
            OP_Rejected = 1051,

            [Description("Risk Approval Pending")]
            Risk_ApprovalPending = 106,

            [Description("Risk Approved")]
            Risk_Approved = 107,

            [Description("Risk Rejected")]
            Risk_Rejected = 1071,

            [Description("Fin Approval Pending")]
            Fin_ApprovalPending = 108,

            [Description("Fin Approved")]
            Fin_Approved = 109,

            [Description("Fin Rejected")]
            Fin_Rejected = 1091,

            [Description("FinOP Approval Pending")]
            FinOP_ApprovalPending = 110,

            [Description("FinOP Approved")]
            FinOP_Approved = 111,

            [Description("FinOP Rejected")]
            FinOP_Rejected = 1111,

            [Description("Position check not through")]
            Withdraw_Position_check_not_through = 1181,

            [Description("Success")]
            Withdraw_Success = 300,

            [Description("Not enough balance")]
            Not_enough_balance = 113,

            [Description("Account can not be found")]
            Withdraw_Account_can_not_be_found = 117,

            [Description("Wrong Signature")]
            Wrong_Signature = 112,

            [Description("Withdraw Disabled")]
            Withdraw_Disabled = 420,

            [Description("Withdraw Inprocess")]
            Withdraw_Inprocess = 421
        }

        public enum DepartmentStatus
        {
            [Description("CustomerService")]
            CustomerService = 102,

            [Description("Operations")]
            Operations = 104,

            [Description("Risk")]
            Risk = 106,

            [Description("Finance")]
            Finance = 108,

            [Description("FinanceOperation")]
            FinanceOperation = 110,
        }

        public enum Account_Type
        {
            C, // Customer
            A, // Agent
            S  // Staff
        }

        public enum Account_Setting_Type
        {
            CHNG_PWD,
            CHNG_LEV,
            RST_PWD,
            CHNG_UN,
            TRNS,
            DEP,
            WHD,
            ALLOW_LOGIN
        }

        public enum Account_Setting_Status
        {
            Pending = 100,
            InProgress = 100,
            Completed = 111,
            Failed = 101,
            Cancelled = 102
        }

        public enum Lead_Status
        {
            Hot,
            Warm,
            Cold
        }

        public enum MonthList
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12,
        }

        public enum ActiveStatus
        {
            [Description("Active")]
            Active = 1,

            [Description("In-Active")]
            InActive = 0,

            [Description("Black Listed")]
            BlackListed = -1,

            [Description("Deleted")]
            Deleted = -2,
        }

        public enum TokenStatus
        {
            Active = 1,
            Used = 2,
            InActive = 0,
            Expired = -1
        }

        public enum Modules
        {
            CRM,
            IB,
            Client,
            CryptoWallet
        }
        public enum Document_Verification_Status
        {
            Pending = 1,
            Manual = 2,
            ReUpload = 3,
            Verified = 4,
            NotVerified = 5,
            Rejected = 6
        }

        public enum OpenAccountKYCSetting
        {
            AfterRegistration,
            AfterVerification,
            AfterKYCProcess
        }
        public enum Customer_TypeCode
        {
            [Description("Individual")]
            CTINDV_0001_0422 = 1,

            [Description("Joint")]
            CTJNT_0002_0422,

            [Description("Corporate")]
            CTCRP_0003_0422
        }
    }
}