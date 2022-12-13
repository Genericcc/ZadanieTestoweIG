using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

namespace Utils.Extensions
{
    public static class DropdownExtensions
    {
        public static void PopulateDropDownWithEnum<T>(this TMP_Dropdown dropdown) where T : Enum
        {
            dropdown.ClearOptions();
            
            var enumType = typeof(T);
            var newOptions = new List<TMP_Dropdown.OptionData>();
 
            for(var i = 0; i < Enum.GetNames(enumType).Length; i++)
            {
                newOptions.Add(new TMP_Dropdown.OptionData(Enum.GetName(enumType, i)));
            }
            dropdown.AddOptions(newOptions);
        }
    }
}