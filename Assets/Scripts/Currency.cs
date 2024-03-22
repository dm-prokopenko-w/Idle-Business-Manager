using System;

public class Termination
{
    public string commas { get; set; }
    public string value { get; set; }
}

public static class Currency
{
    public static string CurrencyText(this double amount)
    {
        int length = (int)Math.Ceiling(Math.Log10(amount));

        Termination[] termination = {
            new Termination { commas = ",", value = "K" },
            new Termination { commas = ",,", value = "M" },
            new Termination { commas = ",,,", value = "B" },
            new Termination { commas = ",,,,", value = "T" },
            new Termination { commas = ",,,,,", value = "Qa" },
            new Termination { commas = ",,,,,,", value = "Qi" },
            new Termination { commas = ",,,,,,,", value = "Sx" },
            new Termination { commas = ",,,,,,,,", value = "Sp" },
            new Termination { commas = ",,,,,,,,,", value = "Oc" },
            new Termination { commas = ",,,,,,,,,,", value = "No" },
            new Termination { commas = ",,,,,,,,,,,", value = "De" },
            new Termination { commas = ",,,,,,,,,,,,", value = "Und" },
            new Termination { commas = ",,,,,,,,,,,,,", value = "Dud" },
            new Termination { commas = ",,,,,,,,,,,,,,", value = "Trd" },
            new Termination { commas = ",,,,,,,,,,,,,,,", value = "Qad" },
            new Termination { commas = ",,,,,,,,,,,,,,,,", value = "Qid" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,", value = "Sxd" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,", value = "Spt" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,", value = "Nod" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,", value = "Vig" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,", value = "Cen" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,", value = "Aa" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,", value = "Bb" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,", value = "Cc" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,", value = "Dd" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Ee" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Ff" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Gg" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Hh" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Ii" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Jj" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Kk" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Ll" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Mm" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Nn" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Oo" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Pp" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Qq" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Rr" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Ss" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Tt" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Uu" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Vv" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Ww" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Xx" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Yy" },
            new Termination { commas = ",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,", value = "Zz" },
        };

        if (length > 89)
        {
            return amount.ToString($"0{termination[46].commas}.##{termination[46].value}");
        }
        else if (length > 88)
        {
            return amount.ToString($"0{termination[45].commas}.##{termination[45].value}");
        }
        else if (length > 87)
        {
            return amount.ToString($"0{termination[44].commas}.##{termination[44].value}");
        }
        else if (length > 86)
        {
            return amount.ToString($"0{termination[43].commas}.##{termination[43].value}");
        }
        else if (length > 85)
        {
            return amount.ToString($"0{termination[42].commas}.##{termination[42].value}");
        }
        else if (length > 84)
        {
            return amount.ToString($"0{termination[41].commas}.##{termination[41].value}");
        }
        else if (length > 83)
        {
            return amount.ToString($"0{termination[40].commas}.##{termination[40].value}");
        }
        else if (length > 82)
        {
            return amount.ToString($"0{termination[39].commas}.##{termination[39].value}");
        }
        else if (length > 81)
        {
            return amount.ToString($"0{termination[38].commas}.##{termination[38].value}");
        }
        else if (length > 80)
        {
            return amount.ToString($"0{termination[37].commas}.##{termination[37].value}");
        }
        else if (length > 79)
        {
            return amount.ToString($"0{termination[36].commas}.##{termination[36].value}");
        }
        else if (length > 78)
        {
            return amount.ToString($"0{termination[35].commas}.##{termination[35].value}");
        }
        else if (length > 77)
        {
            return amount.ToString($"0{termination[34].commas}.##{termination[34].value}");
        }
        else if (length > 76)
        {
            return amount.ToString($"0{termination[33].commas}.##{termination[33].value}");
        }
        else if (length > 75)
        {
            return amount.ToString($"0{termination[32].commas}.##{termination[32].value}");
        }
        else if (length > 74)
        {
            return amount.ToString($"0{termination[31].commas}.##{termination[31].value}");
        }
        else if (length > 73)
        {
            return amount.ToString($"0{termination[30].commas}.##{termination[30].value}");
        }
        else if (length > 72)
        {
            return amount.ToString($"0{termination[29].commas}.##{termination[29].value}");
        }
        else if (length > 71)
        {
            return amount.ToString($"0{termination[28].commas}.##{termination[28].value}");
        }
        else if (length > 70)
        {
            return amount.ToString($"0{termination[27].commas}.##{termination[27].value}");
        }
        else if (length > 69)
        {
            return amount.ToString($"0{termination[26].commas}.##{termination[26].value}");
        }
        else if (length > 68)
        {
            return amount.ToString($"0{termination[25].commas}.##{termination[25].value}");
        }
        else if (length > 67)
        {
            return amount.ToString($"0{termination[24].commas}.##{termination[24].value}");
        }
        else if (length > 66)
        {
            return amount.ToString($"0{termination[23].commas}.##{termination[23].value}");
        }
        else if (length > 65)
        {
            return amount.ToString($"0{termination[22].commas}.##{termination[22].value}");
        }
        else if (length > 64)
        {
            return amount.ToString($"0{termination[21].commas}.##{termination[21].value}");
        }
        else if (length > 63)
        {
            return amount.ToString($"0{termination[20].commas}.##{termination[20].value}");
        }
        else if (length > 60)
        {
            return amount.ToString($"0{termination[19].commas}.##{termination[19].value}");
        }
        else if (length > 57)
        {
            return amount.ToString($"0{termination[18].commas}.##{termination[18].value}");
        }
        else if (length > 54)
        {
            return amount.ToString($"0{termination[17].commas}.##{termination[17].value}");
        }
        else if (length > 51)
        {
            return amount.ToString($"0{termination[16].commas}.##{termination[16].value}");
        }
        else if (length > 48)
        {
            return amount.ToString($"0{termination[15].commas}.##{termination[15].value}");
        }
        else if (length > 45)
        {
            return amount.ToString($"0{termination[14].commas}.##{termination[14].value}");
        }
        else if (length > 42)
        {
            return amount.ToString($"0{termination[13].commas}.##{termination[13].value}");
        }
        else if (length > 39)
        {
            return amount.ToString($"0{termination[12].commas}.##{termination[12].value}");
        }
        else if (length > 36)
        {
            return amount.ToString($"0{termination[11].commas}.##{termination[11].value}");
        }
        else if (length > 33)
        {
            return amount.ToString($"0{termination[10].commas}.##{termination[10].value}");
        }
        else if (length > 30)
        {
            return amount.ToString($"0{termination[9].commas}.##{termination[9].value}");
        }
        else if (length > 27)
        {
            return amount.ToString($"0{termination[8].commas}.##{termination[8].value}");
        }
        else if (length > 24)
        {
            return amount.ToString($"0{termination[7].commas}.##{termination[7].value}");
        }
        else if (length > 21)
        {
            return amount.ToString($"0{termination[6].commas}.##{termination[6].value}");
        }
        else if (length > 18)
        {
            return amount.ToString($"0{termination[5].commas}.##{termination[5].value}");
        }
        else if (length > 15)
        {
            return amount.ToString($"0{termination[4].commas}.##{termination[4].value}");
        }
        else if (length > 12)
        {
            return amount.ToString($"0{termination[3].commas}.##{termination[3].value}");
        }
        else if (length > 9)
        {
            return amount.ToString($"0{termination[2].commas}.##{termination[2].value}");
        }
        else if (length > 6)
        {
            return amount.ToString($"0{termination[1].commas}.##{termination[1].value}");
        }
        else if (length > 3 && amount >= 2000)
        {
            return amount.ToString($"0{termination[0].commas}.##{termination[0].value}");
        }

        return amount.ToString("0.##");
    }
}
