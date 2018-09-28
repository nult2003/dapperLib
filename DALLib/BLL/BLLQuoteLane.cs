using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLib
{
    public class BLLQuoteLane : IBLLQuoteLane
    {
        //private ClassLoggerHelper loggerHelper = new ClassLoggerHelper("BLLQuoteLane");

        //private IBLLCustomerAccount _bllCustomerAccount;
        //private IBLLConcertoAccount _bllConcertoAccount;

        //private IDALQuoteRequest _dalQuoteRequest;
        private IDALQuoteLane _dalQuoteLane;

        public BLLQuoteLane(IDALQuoteLane dalQuoteLane)
        {
            _dalQuoteLane = dalQuoteLane;
        }

        public int CreateLaneForRevised(int quoteRequestId)
        {
            //using (loggerHelper.CreateMethodLogger(quoteRequestId.ToString()))
            //{
            try
            {
                return _dalQuoteLane.CreateLaneForRevised(quoteRequestId);
            }
            catch (Exception e)
            {
            }

            return 0;
            //}
        }

        public bool UpdateCustomerCurrencyId(int quoteLaneId, int customerCurrencyId)
        {
            //using (loggerHelper.CreateMethodLogger($"quoteLaneId={quoteLaneId}, customerCurrencyId={customerCurrencyId}"))
            //{
            try
            {
                return _dalQuoteLane.UpdateCustomerCurrencyId(quoteLaneId, customerCurrencyId);
            }
            catch (Exception e)
            {
                //Log.WriteAppLog(LogEnum.Error, e.Message, e);
            }

            return false;
            //}
        }

        public List<Customers> GetAll()
        {
            //using (loggerHelper.CreateMethodLogger())
            //{
            try
            {
                return _dalQuoteLane.GetAll();
            }
            catch (Exception e)
            {
                //Log.WriteAppLog(LogEnum.Error, e.Message, e);
            }

            return null;
            //}
        }

        public Customers GetForHTO(int id)
        {
            throw new NotImplementedException();
        }

        public Customers GetForQuotationTool(int id)
        {
            throw new NotImplementedException();
        }

        public List<Customers> GetListByCrmId(string crmId)
        {
            throw new NotImplementedException();
        }

        public List<Customers> GetListByCrmIds(List<string> crmIds)
        {
            throw new NotImplementedException();
        }

        public Customers GetForQuotationToolCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public int GetIdByQuoteRequestId(int reqId)
        {
            throw new NotImplementedException();
        }

        public Customers ForHTO(int id)
        {
            throw new NotImplementedException();
        }

        public Customers SetQuoteLaneCodes(int quoteLaneId, string CodeCountry, string CodeYear)
        {
            throw new NotImplementedException();
        }

        public bool QuoteRate_SetNeedValidationByQuoteLane(int quoteLaneId)
        {
            throw new NotImplementedException();
        }

        public Customers GetShipperAndConsigneeForHTOByLane(int id)
        {
            throw new NotImplementedException();
        }

        //List<Customers> ICrud<Customers, int>.GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        public Customers GetSpecific(int id)
        {
            throw new NotImplementedException();
        }

        public bool CreateOrUpdate(Customers element)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        /*
        public QuoteLane GetSpecific(int id)
        {
            using (loggerHelper.CreateMethodLogger($"id={id}"))
            {
                try
                {
                    return _dalQuoteLane.GetSpecific(id);
                }
                catch (Exception e)
                {
                    Log.WriteAppLog(LogEnum.Error, e.Message, e);
                }

                return null;
            }
        }

        public bool Delete(int id)
        {
            using (loggerHelper.CreateMethodLogger($"id={id}"))
            {
                try
                {
                    return _dalQuoteLane.Delete(id);
                }
                catch (Exception e)
                {
                    Log.WriteAppLog(LogEnum.Error, e.Message, e);
                }

                return false;
            }
        }

        public bool CreateOrUpdate(QuoteLane element)
        {
            using (loggerHelper.CreateMethodLogger($"Id={element.Id};QuoteRequestId={element.QuoteRequestId}"))
            {
                try
                {
                    return _dalQuoteLane.CreateOrUpdate(element);
                }
                catch (Exception e)
                {
                    Log.WriteAppLog(LogEnum.Error, e.Message, e);
                }

                return false;
            }
        }
        public QuoteLane GetForHTO(int id)
        {
            using (loggerHelper.CreateMethodLogger($"Id={id}"))
            {
                try
                {
                    QuoteLane lane = null;

                    lane = _dalQuoteLane.GetForHTO(id);

                    return lane;
                }
                catch (Exception e)
                {
                    Log.WriteAppLog(LogEnum.Error, e.Message, e);
                }
                return null;
            }
        }

        public QuoteLane GetForQuotationTool(int id)
        {
            using (loggerHelper.CreateMethodLogger($"Id={id}"))
            {
                try
                {
                    QuoteLane res = null;

                    res = _dalQuoteLane.GetForQuotationTool(id);
                    res.QuoteRates.RemoveAll(x => x.QuoteRateStatus.Code == "CNL");

                    if (res != null && res.QuoteRequest != null)
                    {
                        res.QuoteRequest.ContactIds = _dalQuoteRequest.GetQuoteRequestContacts(res.QuoteRequest.Id);

                        //if (_bllConcertoAccount == null)
                        //    _bllConcertoAccount = new BLLConcertoAccount();

                        var concertoAccount = _bllConcertoAccount.GetConcertoAccountByCrmId(res.QuoteRequest.CrmId);

                        if (concertoAccount != null)
                        {
                            //if(_bllCustomerAccount == null)
                            //    _bllCustomerAccount = new BLLCustomerAccount();

                            var account = _bllCustomerAccount.GetByDTOAccount(concertoAccount);
                            res.QuoteRequest.CustomerAccount = account;
                            //if (!string.IsNullOrEmpty(res.QuoteRequest.ContactId))
                            //    res.QuoteRequest.CustomerContact = res.QuoteRequest.CustomerAccount.CustomerContacts.FirstOrDefault(x => x.ContactId == res.QuoteRequest.ContactId);
                            if (res.QuoteRequest.ContactIds.Count != 0)
                                res.QuoteRequest.CustomerContacts = res.QuoteRequest.CustomerAccount.CustomerContacts.Where(x => res.QuoteRequest.ContactIds.Contains(x.ContactId)).ToList();
                        }
                    }
                    return res;
                }
                catch (Exception e)
                {
                    Log.WriteAppLog(LogEnum.Error, e.Message, e);
                }
                return null;
            }
        }

        public List<QuoteLane> GetListByCrmId(string crmId)
        {
            using (loggerHelper.CreateMethodLogger($"crmId={crmId}"))
            {
                try
                {
                    List<QuoteLane> res = new List<QuoteLane>();

                    res = _dalQuoteLane.GetListByCrmId(crmId);
                    res.ForEach(x => x.QuoteRates.RemoveAll(y => y.QuoteRateStatus.Code == "CNL"));

                    return res;
                }
                catch (Exception e)
                {
                    Log.WriteAppLog(LogEnum.Error, e.Message, e);
                }
                return null;
            }
        }

        public List<QuoteLane> GetListByCrmIds(List<string> crmIds)
        {
            using (loggerHelper.CreateMethodLogger($"crmIds={string.Join(",", crmIds)}"))
            {
                try
                {
                    List<QuoteLane> res = new List<QuoteLane>();

                    foreach (var crmId in crmIds)
                    {
                        var r = _dalQuoteLane.GetListByCrmId(crmId);
                        res.AddRange(r);
                    }
                    res.ForEach(x => x.QuoteRates.RemoveAll(y => y.QuoteRateStatus.Code == "CNL"));

                    return res;
                }
                catch (Exception e)
                {
                    Log.WriteAppLog(LogEnum.Error, e.Message, e);
                }
                return null;
            }
        }

        public QuoteLane GetForQuotationToolCustomer(int id)
        {
            using (loggerHelper.CreateMethodLogger($"Id={id}"))
            {
                try
                {
                    QuoteLane lane = null;

                    lane = _dalQuoteLane.GetForQuotationToolCustomer(id);

                    if (lane != null && lane.QuoteRequest != null)
                    {
                        lane.QuoteRequest.ContactIds = _dalQuoteRequest.GetQuoteRequestContacts(lane.QuoteRequest.Id);

                        //if (_bllConcertoAccount == null)
                        //    _bllConcertoAccount = new BLLConcertoAccount();

                        var concertoAccount = _bllConcertoAccount.GetConcertoAccountByCrmId(lane.QuoteRequest.CrmId);

                        if (concertoAccount != null)
                        {
                            //if (_bllCustomerAccount == null)
                            //    _bllCustomerAccount = new BLLCustomerAccount();

                            var account = _bllCustomerAccount.GetByDTOAccount(concertoAccount);

                            lane.QuoteRequest.CustomerAccount = account;
                            //if (!string.IsNullOrEmpty(lane.QuoteRequest.ContactId))
                            //    lane.QuoteRequest.CustomerContact = lane.QuoteRequest.CustomerAccount.CustomerContacts.FirstOrDefault(x => x.ContactId == lane.QuoteRequest.ContactId);
                            if (lane.QuoteRequest.ContactIds.Count != 0)
                                lane.QuoteRequest.CustomerContacts = lane.QuoteRequest.CustomerAccount.CustomerContacts.Where(x => lane.QuoteRequest.ContactIds.Contains(x.ContactId)).ToList();
                        }
                    }
                    return lane;
                }
                catch (Exception e)
                {
                    Log.WriteAppLog(LogEnum.Error, e.Message, e);
                }
                return null;
            }
        }

        public int GetIdByQuoteRequestId(int reqId)
        {
            using (loggerHelper.CreateMethodLogger($"reqId={reqId}"))
            {
                try
                {
                    return _dalQuoteLane.GetIdByQuoteRequestId(reqId);
                }
                catch (Exception e)
                {
                    Log.WriteAppLog(LogEnum.Error, e.Message, e);
                }

                return 0;
            }
        }

        public QuoteLane ForHTO(int id)
        {
            using (loggerHelper.CreateMethodLogger($"Id={id}"))
            {
                try
                {
                    var res = _dalQuoteLane.ForHTO(id);
                    return res;
                }
                catch (Exception e)
                {
                    Log.WriteAppLog(LogEnum.Error, e.Message, e);
                }
                return null;
            }
        }

        public QuoteLane SetQuoteLaneCodes(int quoteLaneId, string CodeCountry, string CodeYear)
        {
            using (loggerHelper.CreateMethodLogger())
            {
                try
                {
                    return _dalQuoteLane.SetQuoteLaneCodes(quoteLaneId, CodeCountry, CodeYear);
                }
                catch (Exception e)
                {
                    Log.WriteAppLog(LogEnum.Error, e.Message, e);
                }
            }
            return null;
        }
        public bool QuoteRate_SetNeedValidationByQuoteLane(int quoteLaneId)
        {
            using (loggerHelper.CreateMethodLogger($"quoteLaneId={quoteLaneId}"))
            {
                try
                {
                    return _dalQuoteLane.SetNeedValidationByQuoteLane(quoteLaneId);
                }
                catch (Exception e)
                {
                    Log.WriteAppLog(LogEnum.Error, e.Message, e);
                }
            }
            return false;
        }

        public QuoteLane GetShipperAndConsigneeForHTOByLane(int id)
        {
            using (loggerHelper.CreateMethodLogger($"Id={id}"))
            {
                try
                {
                    QuoteLane lane = null;

                    lane = _dalQuoteLane.GetShipperAndConsigneeForHTOByLane(id);

                    return lane;
                }
                catch (Exception e)
                {
                    Log.WriteAppLog(LogEnum.Error, e.Message, e);
                }
                return null;
            }
        }

        */
    }
}
