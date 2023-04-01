using MarvelApi.ViewModels;

namespace MarvelApi.Interfaces
{
    public interface IMarvelAppService
    {
        bool GravarPersonagens(CaracteresMarvelViewModel viewModel);
    }
}
