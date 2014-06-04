// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

namespace Gma.Drivers.Lego.IrRc
{
    public enum DirectState
    {
        RedFlt = 0x0,
        RedFwd = 0x1,
        RedRev = 0x2,
        RedBrk = 0x3,
        BlueFlt = 0x0,
        BlueFwd = 0x4,
        BlueRev = 0x8,
        BlueBrk = 0xC
    };
}