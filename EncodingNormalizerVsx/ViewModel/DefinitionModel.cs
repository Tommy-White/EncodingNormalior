using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EncodingNormalior.Model;
using Newtonsoft.Json;

namespace EncodingNormalizerVsx.ViewModel
{
    /// <summary>
    ///     �����û�����
    /// </summary>
    public class DefinitionModel : NotifyProperty
    {
        /// <summary>
        /// �����û������ļ���
        /// </summary>
        private static readonly string _folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\EncodingNormalizer\\";
        /// <summary>
        /// �����û������ļ�
        /// </summary>
        private static readonly string _file = _folder + "Account.json";

        private Account _account;

        public DefinitionModel()
        {
            //һ�����
            if (Account == null)
            {
                ReadAccount();
            }

            OptionCriterionEncoding = new List<string>();
            foreach (var temp in Enum.GetNames(typeof(CriterionEncoding)))
            {
                OptionCriterionEncoding.Add(temp);
            }

            //��ȡ֮ǰ�ı���
            CriterionEncoding = OptionCriterionEncoding.First(temp => temp.Equals(Account.CriterionEncoding.ToString()));
        }

        /// <summary>
        /// ��ѡ�ı���
        /// </summary>
        public List<string> OptionCriterionEncoding { set; get; }

        private string _criterionEncoding;

        public string CriterionEncoding
        {
            set
            {
                _criterionEncoding = value;
                OnPropertyChanged();
            }
            get
            {
                return _criterionEncoding;
            }
        }
        /// <summary>
        ///     �û�����
        /// </summary>
        public Account Account
        {
            set
            {
                _account = value;
                OnPropertyChanged();
            }
            get { return _account; }
        }

        /// <summary>
        ///     ��ȡ����
        /// </summary>
        private void ReadAccount()
        {
            Account = Account.ReadAccount();
        }


        /// <summary>
        ///     д���û�����
        /// </summary>
        public bool WriteAccount()
        {
            //todo: У��Account
            ViewModel.CriterionEncoding criterionEncoding;
            /*Account.CriterionEncoding =*/
            if (ViewModel.CriterionEncoding.TryParse(CriterionEncoding, out criterionEncoding))
            {
                Account.CriterionEncoding = criterionEncoding;
            }
            
            try
            {
                Account.WriteAccount();
            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }
    }
}