// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

namespace Gma.Netduino.R2D2.Drivers.LegoInfrared.Internal
{
    public enum RemoteControlMode
    {
        ComboDirectMode = 0x1,
        SinglePinContinuous = 0x2,
        SinglePinTimeout = 0x3,
        SingleOutput = 0x4
    };
}