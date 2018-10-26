using System;
using System.Data;

namespace dapperRepositoryLib
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// 
        /// </summary>
        //private IAddressBookRepository _addressBookRepository;
        ///// <summary>
        ///// 
        ///// </summary>
        //private IHelpListRepository _helpListRepository;
        ///// <summary>
        ///// 
        ///// </summary>
        //private IClientRequestRepository _clientRequestRepository;
        /// <summary>
        /// 
        /// </summary>
        private IProfileRepository _profileRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public UnitOfWork(IDbConnection connection)
        {
            _id = Guid.NewGuid();
            _connection = connection;
        }

        IDbConnection _connection = null;
        IDbTransaction _transaction = null;
        Guid _id = Guid.Empty;

        IDbConnection IUnitOfWork.Connection
        {
            get { return _connection; }
        }
        IDbTransaction IUnitOfWork.Transaction
        {
            get { return _transaction; }
        }
        Guid IUnitOfWork.Id
        {
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        //public IAddressBookRepository AddressBookRepository
        //{
        //    get { return _addressBookRepository ?? (_addressBookRepository = new AddressBookRepository(_connection, _transaction)); }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //public IHelpListRepository HelpListRepository
        //{
        //    get { return _helpListRepository ?? (_helpListRepository = new HelpListRepository(_connection, _transaction)); }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //public IClientRequestRepository ClientRequestRepository
        //{
        //    get { return _clientRequestRepository ?? (_clientRequestRepository = new ClientRequestRepository(_connection, _transaction)); }
        //}

        /// <summary>
        /// 
        /// </summary>
        public IProfileRepository ProfileRepository
        {
            get { return _profileRepository ?? (_profileRepository = new ProfileRepository(_connection, _transaction)); }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Begin()
        {
            _transaction = _connection.BeginTransaction();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();
            _transaction = null;
        }
    }
}
