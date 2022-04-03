using Prism.Events;
using System;

namespace ClassesLibrary.Classes
{
    public class SendLanguageEvent : PubSubEvent<System.Globalization.CultureInfo>
    {
    }
}
