using System.Threading.Tasks;

namespace Framework.Persistence
{
    public interface IUnitOfWork : System.IDisposable
	{
		bool IsDisposed { get; }

		Task<int> SaveAsync();
	}
}
