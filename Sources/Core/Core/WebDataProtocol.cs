namespace Core;

static public class WebDataProtocol
{

    public struct Message
    {
        public string type;
        public string data;
    }


    public struct RegisterUser
    {
        public string name;
        public string password;
    }
}
