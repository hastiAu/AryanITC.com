using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPE.SmsIrRestful.TPL.NetCore;

namespace AryanITC.Core.Senders
{
    public class SendVerificationCodeSms
    {
        private static readonly string ApiKey = "9e4dbfb22e6c8b6eb8bfbfb1";
        private static readonly string SecretKey = "asds5463fdSFrgryuPIKJK";

        public static bool SendVerificationCode(string mobile, string code)
        {
            var token = new Token().GetToken(ApiKey, SecretKey);
            var restVerificationCode = new RestVerificationCode()
            {
                Code = code,
                MobileNumber = mobile
            };
            var restVerificationCodeResponse = new VerificationCode().Send(token, restVerificationCode);
            if (restVerificationCodeResponse.IsSuccessful)
            {
                return restVerificationCodeResponse.IsSuccessful;
                //return VerificationCodeResult.SentSuccessfully;
            }

            //return VerificationCodeResult.Error;
            return false;
        }
    }
}
