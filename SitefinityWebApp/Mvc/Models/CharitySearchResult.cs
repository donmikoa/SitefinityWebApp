using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp.Mvc.Models
{
    public sealed class CharitySearchResult
    {
        public CharitySearchResult(string title, string address, string city, string state, string postalcode, int? distance, bool accredited, bool sealholder, string classificationtext)
        {
            this._name = title;
            this._address = address;
            this._city = city;
            this._state = state;
            this._postalcode = postalcode;
            if (distance.HasValue)
            {
                this._distance = (int)distance;
            }
            this._isAccredited = accredited;
            this._isSealHolder = sealholder;
            this._category = classificationtext;
        }

        public string Name { get { return this._name; } }
        public string Address { get { return this._address; } }
        public string City { get { return this._city; } }
        public string State { get { return this._state; } }
        public string PostalCode { get { return this._postalcode; } }
        public int Distance { get { return this._distance; } }
        public string Category { get { return this._category; } }
        public bool IsAccredited { get { return this._isAccredited; } }
        public bool IsSealHolder { get { return this._isSealHolder; } }

        private readonly string _name;
        private readonly string _address;
        private readonly string _city;
        private readonly string _state;
        private readonly string _postalcode;
        private readonly int _distance;
        private readonly bool _isAccredited;
        private readonly bool _isSealHolder;
        private readonly string _category;
    }
}