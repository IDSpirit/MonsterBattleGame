using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MonsterBattleUI.Models
{
    [ValueConversion(typeof(List<string>), typeof(string))]
    public class ListToStringConverter
    {
        public object Convert(List<string> value, Type targetType, int turnNum)
        {
            if (targetType != typeof(string))
                throw new InvalidOperationException("The target needs to be of string format");

            value.Insert(0, "\nTurn " + turnNum);
            var stringVar = string.Join(" \n", ((List<string>) value).ToArray());
            //stringVar.Concat("\n---------------------\n");
            //stringVar.Insert(-1, "\n---------------------\n");
            return stringVar;
        }

        public object ConvertBack(object value, Type targetType)
        {
            throw new NotImplementedException();
        }
    }
}
