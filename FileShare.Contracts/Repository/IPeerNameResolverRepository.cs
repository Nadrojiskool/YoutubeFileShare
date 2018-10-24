using FileShare.Domain.Models;

namespace FileShare.Contracts.Repository
{
    public interface IPeerNameResolverRepository
    {
        void ResolvePeerName(string peerID);

        PeerEndpointCollection PeerEndpointCollection { get; set; }
    }
}
