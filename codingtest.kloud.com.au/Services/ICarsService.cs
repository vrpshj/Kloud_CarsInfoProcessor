using System.Threading.Tasks;

namespace codingtest.kloud.com.au
{
    internal interface ICarsService
    {
        string GetCarsJson();
        string[] GetOwnersByColour(string colourname);
        string GetCarsJsonByOwner(string ownername);
        string[] GetOwnersByBrand(string brandname);
    }
}