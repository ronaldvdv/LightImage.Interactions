using System.Threading.Tasks;

namespace LightImage.Interactions
{
    public interface IInteractionTask : IInteractionInput<bool>
    {
        Task Execute();
    }
}