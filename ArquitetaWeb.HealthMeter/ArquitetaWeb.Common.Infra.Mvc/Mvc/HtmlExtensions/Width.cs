
namespace ArquitetaWeb.Common.Infra.Mvc
{
    public enum Width
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Eleven,
        Twelve,
    }

    public static class WidthExtensions
    {
        public static string ToSpanString(this Width width)
        {
            string span;

            switch (width)
            {
                case Width.One:
                    span = "col-xs-1";
                    break;
                case Width.Two:
                    span = "col-xs-2";
                    break;
                case Width.Three:
                    span = "col-xs-3";
                    break;
                case Width.Four:
                    span = "col-xs-4";
                    break;
                case Width.Five:
                    span = "col-xs-5";
                    break;
                case Width.Six:
                    span = "col-xs-6";
                    break;
                case Width.Seven:
                    span = "col-xs-7";
                    break;
                case Width.Eight:
                    span = "col-xs-8";
                    break;
                case Width.Nine:
                    span = "col-xs-9";
                    break;
                case Width.Ten:
                    span = "col-xs-10";
                    break;
                case Width.Eleven:
                    span = "col-xs-11";
                    break;
                case Width.Twelve:
                    span = "col-xs-12";
                    break;
                default:
                    span = "col-xs-2";
                    break;
            }
            return span;
        }
    }
}
