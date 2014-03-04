using System;

namespace SolarSystem.Mars.ViewController.Exceptions
{
    public class InvalidModelStateException : Exception
    {
        public string DisplayMessage { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public InvalidModelStateException(string message = null)
        {
            DisplayMessage = message;
        }
    }
}