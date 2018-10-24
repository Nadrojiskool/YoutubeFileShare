using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileShare.Contracts.Repository;
using System.Net.PeerToPeer;

namespace FileShare.Logic.PnrpManager
{
    public class PeerRegistrationManager : IPeerRegistrationRepository
    {
        #region field
        private PeerNameRegistration _peerNameRegistration = null;
        #endregion

        public bool IsPeerRegistered => _peerNameRegistration != null && _peerNameRegistration.IsRegistered();
        public string PeerUri => GetPeerUri();
        public PeerName PeerName { get; set; }

        private string GetPeerUri()
        {
            return _peerNameRegistration?.PeerName.PeerHostName;
        }

        public void StartPeerRegistration(string username, int port)
        {
            PeerName = new PeerName(username, PeerNameType.Unsecured);
            _peerNameRegistration = new PeerNameRegistration(PeerName, port);
            _peerNameRegistration.Start();
        }

        public void StopPeerRegistration()
        {
            if (_peerNameRegistration != null)
            {
                _peerNameRegistration?.Stop();
                _peerNameRegistration = null;
            }
        }
    }
}
