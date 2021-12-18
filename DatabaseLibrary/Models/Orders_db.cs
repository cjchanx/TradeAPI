﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.Models
{
    public class Orders_db
    {
        #region Constructors
        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public Orders_db() { }

        /// <summary>
        /// orders
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="AccountRef"></param>
        /// <param name="Action"></param>
        /// <param name="DateCreated"></param>
        /// <param name="Quantity"></param>
        /// <param name="Status"></param>
        /// <param name="Symbol"></param>
        public Orders_db(int Id, int AccountRef, int Action, DateTime DateCreated, int Quantity, int Status, string Symbol)
        {
            this.Id = Id;
            this.AccountRef = AccountRef;
            this.Action = Action;
            this.DateCreated = DateCreated;
            this.Quantity = Quantity;
            this.Status = Status;
            this.Symbol = Symbol;
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        public int AccountRef { get; set; }
        public int Action { get; set; }

        public DateTime DateCreated { get; set; }

        public int Quantity { get; set; }

        public int Status { get; set; }

        public string Symbol { get; set; }

        #endregion
    }
}