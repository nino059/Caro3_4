﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caro3_4.Entity
{
    public class Player
    {
        private string? name;

        public string Name
        {
            get
            {
                return name!;
            }
            set
            {
                name = value;
            }
        }
        private Image? mark;

        public Image Mark { get { return mark!; } set { mark = value; } }
        public Player(string name, Image mark)
        {
            Name = name;
            Mark = mark;
        }

    }
}
