using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineSinav.Models;

namespace OnlineSinav.Models
{
    public class Cart
    {
        private List<CartLine> _cartlines = new List<CartLine>();
        public List<CartLine> CartLines
        {
            get { return _cartlines; }
        }
        public void AddQuestion(Soru soru, int quantity)
        {
            var line = _cartlines.FirstOrDefault(i => i.Soru.Id == soru.Id);
            if (line == null)
            {
                _cartlines.Add(new CartLine()
                {
                    Soru = soru,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }

        }

        public void DelecetQuestion(Soru soru)
        {
            _cartlines.RemoveAll(i => i.Soru.Id == soru.Id);
        }

        public double Total()
        {
            return _cartlines.Sum(i => i.Soru.Puan * i.Quantity);
        }

        public void Clear()
        {
            _cartlines.Clear();
        }
    }

    public class CartLine
    {
        public Soru Soru { get; set; }
        public int Quantity { get; set; }
    }
}