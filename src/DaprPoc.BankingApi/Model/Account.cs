// ------------------------------------------------------------
// Licensed under the MIT License.
// ------------------------------------------------------------

namespace DaprPoc.BankingApi.Model
{
    /// <summary>
    /// Class representing an Account
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Gets or sets account id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets account balance.
        /// </summary>
        public decimal Balance { get; set; }
    }
}