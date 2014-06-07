// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System.Threading;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Actuators;

#endregion

namespace Gma.Netduino.R2D2
{
    public class Robot
    {
        private const int BarrierDistance = 20;
        private readonly Led m_AlarmLed;
        private readonly Motor m_DriveMotor;
        private readonly bool m_IsBlocked;
        private readonly Sonar m_Sonar;
        private readonly Servo m_SteeringWeel;

        public Robot(Motor driveMotor, Servo steeringWeel, Sonar sonar, Led alarmLed)
        {
            m_IsBlocked = false;
            m_DriveMotor = driveMotor;
            m_SteeringWeel = steeringWeel;
            m_Sonar = sonar;
            m_AlarmLed = alarmLed;
        }

        public bool IsBlocked()
        {
            return m_IsBlocked;
        }

        public bool HasBarrier()
        {
            int distance;
            var succeed = m_Sonar.TryGetDistance(out distance);
            if (!succeed)
            {
                m_AlarmLed.TurnOn();
                return true;
            }
            return distance < BarrierDistance;
        }

        public void StepForward()
        {
            m_DriveMotor.SetSpeed(4);
            Thread.Sleep(100);
        }

        public void Reset()
        {
            m_AlarmLed.TurnOff();
            m_DriveMotor.Brake();
            m_SteeringWeel.Center();
        }

        public void TurnRight()
        {
            m_SteeringWeel.SetAngle180(180);
        }

        public void StepBackward()
        {
            m_DriveMotor.SetSpeed(12);
            Thread.Sleep(100);
        }
    }
}