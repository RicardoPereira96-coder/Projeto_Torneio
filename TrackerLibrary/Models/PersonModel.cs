using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    /// <summary>
    /// Representa uma pessoa
    /// </summary>
    public class PersonModel
    {
        public int Id { get; set; }
        /// <summary>
        /// O primeiro noma da pessoa
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// O ultimo do da pessoa
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///  O email da pessoa
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// O telefone da pessoa
        /// </summary>
        public string CellphoneNumber { get; set; }

        public string FullName
        {
            get
            {
                return $"{ FirstName } { LastName }";
            }
        }
    }
}
