// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

namespace Gma.Drivers.Lego.IrRc.Advanced
{
    public abstract class Command
    {
        public abstract CommandType CommandType { get; }
    }
}