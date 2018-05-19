using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace wxweb
{
    /// <summary>
    /// 加密算法处理类
    /// </summary>
    /// <remarks>
    /// <para>这里提供了五种加密算法，分别为Des、Rc2、TripleDes、Rijndael、Md5。</para>
    /// <para>
    ///	Md5算法无需设定秘钥，其余四种都需要设定秘钥且对秘钥的长度有一定的限制，Des算法为8位，Rc2算法为16位，TripleDes算法为24位，
    ///	Rijndael算法为32位，TripleDes算法的秘钥必须是一些特殊字符、字母、数字的混合体
    /// </para>
    /// </remarks>
    public class Encryption
    {

        public static string Encrypt(string text, string passphrase, string salt, EncryptionType encryptionType)
        {
            switch (encryptionType)
            {
                case EncryptionType.None:
                    return text;
                case EncryptionType.AesEncryption:
                    return AESEncryption.Encrypt<AesManaged>(text, passphrase, salt);
                default:
                    return text;
            }
        }

        public static string Decrypt(string text, string passphrase, string salt, EncryptionType encryptionType)
        {
            switch (encryptionType)
            {
                case EncryptionType.None:
                    return text;
                case EncryptionType.AesEncryption:
                    return AESEncryption.Decrypt<AesManaged>(text, passphrase, salt);
                default:
                    return text;
            }
        }

        #region variable

        private EncryptionAlgorithm m_alGid;
        private const string IV_STRING = "YTDInfo.XJTLU.DOMATE";

        #endregion

        #region functions

        /// <summary>
        /// 加密算法构造函数，制定算法类型
        /// </summary>
        /// <param name="algid">加密算法的类型</param>
        public Encryption(EncryptionAlgorithm algid)
        {
            this.m_alGid = algid;
        }

        /// <summary>
        /// 加密函数
        /// </summary>
        /// <example>
        /// <code>
        /// Encryption oEncryption = new Encryption(EncryptionAlgorithm.TripleDes);
        /// string sVal = "encryption";
        /// string sKey = "123456781#$4567812345678";
        /// //加密
        /// string sResult = oEncryption.encrypt(sVal, sKey);
        /// System.Console.WriteLine(sResult);
        /// //解密
        /// sResult = oEncryption.decrypt(sResult, sKey);
        /// System.Console.WriteLine(sResult);
        /// </code>
        /// </example>
        /// <param name="encrytString">加密字符串</param>
        /// <param name="key">秘钥: Des算法为8位，Rc2算法为16位，TripleDes算法为24位且必须是一些特殊字符、字母、数字的混合体，Rijndael算法为32位，Md5无需设定传null之值</param>
        /// <returns>加密后的字符串</returns>
        public string encrypt(string encrytString, string key)
        {
            Encryptor ency = new Encryptor(this.m_alGid);
            if (this.m_alGid.Equals(EncryptionAlgorithm.Rijndael))
                ency.IV = Encoding.ASCII.GetBytes(this.convertString(IV_STRING, 16));
            else
                ency.IV = Encoding.ASCII.GetBytes(this.convertString(IV_STRING, 8));
            return Convert.ToBase64String(ency.Encrypt(Encoding.ASCII.GetBytes(encrytString),
                Encoding.ASCII.GetBytes(this.getCorrectKeyString(key, this.m_alGid))));
        }

        /// <summary>
        /// 加密函数
        /// </summary>
        /// <param name="encrytString">加密字符串</param>
        /// <returns>加密后的字符串</returns>
        public string encrypt(string encrytString)
        {
            return this.encrypt(encrytString, "23452#376%EPS");
        }

        /// <summary>
        /// 解密函数
        /// </summary>
        /// <code>
        /// Encryption oEncryption = new Encryption(EncryptionAlgorithm.TripleDes);
        /// string sVal = "encryption";
        /// string sKey = "123456781#$4567812345678";
        /// //加密
        /// string sResult = oEncryption.encrypt(sVal, sKey);
        /// System.Console.WriteLine(sResult);
        /// //解密
        /// sResult = oEncryption.decrypt(sResult, sKey);
        /// System.Console.WriteLine(sResult);
        /// </code>
        /// <param name="encrytedString">解密字符串</param>
        /// <param name="key">秘钥: Des算法为8位，Rc2算法为16位，TripleDes算法为24位且必须是一些特殊字符、字母、数字的混合体，Rijndael算法为32位，Md5无需设定传null之值</param>
        /// <returns>加密后的字符串</returns>
        public string decrypt(string encrytedString, string key)
        {
            Decryptor decy = new Decryptor(this.m_alGid);
            if (this.m_alGid.Equals(EncryptionAlgorithm.Rijndael))
                decy.IV = Encoding.ASCII.GetBytes(this.convertString(IV_STRING, 16));
            else
                decy.IV = Encoding.ASCII.GetBytes(this.convertString(IV_STRING, 8));
            return Encoding.ASCII.GetString(decy.Decrypt(Convert.FromBase64String(encrytedString),
                Encoding.ASCII.GetBytes(this.getCorrectKeyString(key, this.m_alGid))));
        }

        /// <summary>
        /// 解密函数
        /// </summary>
        /// <param name="encrytedString">解密字符串</param>
        /// <returns>加密后的字符串</returns>
        public string decrypt(string encrytedString)
        {
            return this.decrypt(encrytedString, "23452#376%EPS");
        }
        /// <summary>
        /// this function provide to get the correct key string 
        /// </summary>
        /// <param name="val">key</param>
        /// <param name="algid">EncryptionAlgorithm</param>
        /// <returns>correct key string</returns>
        private string getCorrectKeyString(string val, EncryptionAlgorithm algid)
        {
            if (val == null)
                val = IV_STRING;
            switch (algid)
            {
                case EncryptionAlgorithm.Des:
                    return this.convertString(val, 8);
                case EncryptionAlgorithm.Rc2:
                    return this.convertString(val, 16);
                case EncryptionAlgorithm.TripleDes:
                    return this.convertString(val, 24);
                case EncryptionAlgorithm.Rijndael:
                    return this.convertString(val, 32);
                case EncryptionAlgorithm.Md5:
                    return this.convertString(val, 8);
                default:
                    return null;
            }
        }

        /// <summary>
        /// this function provide to convert string by certain length
        /// </summary>
        /// <param name="val">string</param>
        /// <param name="len">length</param>
        /// <returns>certain length string</returns>
        private string convertString(string val, int len)
        {
            if (val.Length == len)
                return val;
            if (val.Length > len)
                return val.Substring(0, len);
            if (val.Length < len)
            {
                int n = len - val.Length;
                for (int i = 0; i < n; i++)
                {
                    val += "s";
                    if (val.Length == len)
                        return val;
                }
            }
            return val;
        }
        #endregion

        #region  MD5加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="ConvertString">要加密的字符串</param>
        /// <returns></returns>
        public static string GetMd5Str(string ConvertString)
        {
            string pwd = string.Empty;
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(ConvertString));
            for (int i = 0; i < s.Length; i++)
            {
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }

        /// <summary>
        /// MD5加密 支付使用 需要和java的一致性对应上
        /// </summary>
        /// <param name="convertString"></param>
        /// <returns></returns>
        public static string GetPayMd5Str(string convertString)
        {
            byte[] result = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(Encoding.UTF8.GetBytes(convertString));
            StringBuilder output = new StringBuilder(16);
            for (int i = 0; i < result.Length; i++)
            {
                output.Append((result[i]).ToString("x2", System.Globalization.CultureInfo.InvariantCulture));
            }
            return output.ToString();
        }


        /// <summary>
        ///32位 md5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5_32(string str)
        {
            byte[] arrHashInput;
            byte[] arrHashOutput;
            //MD5CryptoServiceProvider objMD5  = new MD5CryptoServiceProvider();
            MD5 objMD5 = new MD5CryptoServiceProvider();
            arrHashInput = C2B(str);
            arrHashOutput = objMD5.ComputeHash(arrHashInput);
            return BitConverter.ToString(arrHashOutput);
        }
        protected static byte[] C2B(string str)
        {
            char[] arrChar;
            arrChar = str.ToCharArray();
            byte[] arrByte = new byte[arrChar.Length];
            for (int i = 0; i < arrChar.Length; i++)
            {
                arrByte[i] = Convert.ToByte(arrChar[i]);
            }
            return arrByte;
        }

        /// <summary>
        /// 16位 MD5加密
        /// </summary>
        /// <param name="sInputString"></param>
        /// <returns></returns>
        public static string GetMD5_16(string sInputString)
        {
            byte[] data = Encoding.UTF8.GetBytes(sInputString);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            string sKey = GenerateKey();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return BitConverter.ToString(result);
        }
        // 创建Key
        public static string GenerateKey()
        {
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }
        // 加密字符串
        public static string EncryptString(string sInputString, string sKey)
        {
            byte[] data = Encoding.UTF8.GetBytes(sInputString);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return BitConverter.ToString(result);
        }
        // 解密字符串
        public static string DecryptString(string sInputString, string sKey)
        {
            string[] sInput = sInputString.Split("-".ToCharArray());
            byte[] data = new byte[sInput.Length];
            for (int i = 0; i < sInput.Length; i++)
            {
                data[i] = byte.Parse(sInput[i], NumberStyles.HexNumber);
            }
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateDecryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return Encoding.UTF8.GetString(result);
        }
        #endregion

        #region Base64

        public static string EncodeBase64(string str)
        {
            System.Text.Encoding pEncoding = System.Text.Encoding.GetEncoding("utf-8");
            return System.Convert.ToBase64String(pEncoding.GetBytes(str)).Replace("+", "|");
        }

        public static string DecodeBase64(string str)
        {
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("utf-8");
            return encoding.GetString(System.Convert.FromBase64String(str.Replace("|", "+")));
        }

        #endregion

        #region 用于支付的AES加密解密

        /// <summary>
        /// 有密码的AES加密 
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="password">加密的密码</param>
        /// <param name="iv">密钥</param>
        /// <returns></returns>
        public static string PayAESEncrypt(string toEncrypt, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string PayAESDecrypt(string toDecrypt, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        #endregion
    }

    public enum EncryptionType
    {
        None,
        AesEncryption
    }

    public static class AESEncryption
    {
        public static string Encrypt<T>(string value, string passphrase, string salt) where T : SymmetricAlgorithm, new()
        {
            DeriveBytes rgb = new Rfc2898DeriveBytes(passphrase, Encoding.Unicode.GetBytes(salt));

            SymmetricAlgorithm algorithm = new T();

            var rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
            var rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);

            var transform = algorithm.CreateEncryptor(rgbKey, rgbIV);

            using (var buffer = new MemoryStream())
            {
                using (var stream = new CryptoStream(buffer, transform, CryptoStreamMode.Write))
                {
                    using (var writer = new StreamWriter(stream, Encoding.Unicode))
                    {
                        writer.Write(value);
                    }
                }

                return Convert.ToBase64String(buffer.ToArray());
            }
        }

        public static string Decrypt<T>(string text, string passphrase, string salt) where T : SymmetricAlgorithm, new()
        {
            DeriveBytes rgb = new Rfc2898DeriveBytes(passphrase, Encoding.Unicode.GetBytes(salt));

            SymmetricAlgorithm algorithm = new T();

            var rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
            var rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);

            var transform = algorithm.CreateDecryptor(rgbKey, rgbIV);

            using (var buffer = new MemoryStream(Convert.FromBase64String(text)))
            {
                using (var stream = new CryptoStream(buffer, transform, CryptoStreamMode.Read))
                {
                    using (var reader = new StreamReader(stream, Encoding.Unicode))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }

    #region 
    /// <summary>
    /// Encryptor
    /// </summary>
    internal class Encryptor
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="algId">Encryption Algorithm</param>
        public Encryptor(EncryptionAlgorithm algId)
        {
            transformer = new EncryptTransformer(algId);
        }

        private EncryptTransformer transformer;
        private byte[] initVec;
        private byte[] encKey;

        /// <summary>
        /// get or set iv
        /// </summary>
        public byte[] IV
        {
            get { return initVec; }
            set { initVec = value; }
        }

        /// <summary>
        /// get or set key
        /// </summary>
        public byte[] Key
        {
            get { return encKey; }
        }

        /// <summary>
        /// this function provide to encrypt the data
        /// </summary>
        /// <param name="bytesData">data</param>
        /// <param name="bytesKey">key</param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] bytesData, byte[] bytesKey)
        {
            // 设置将保存加密数据的流

            MemoryStream memStreamEncryptedData = new MemoryStream();

            transformer.IV = initVec;
            ICryptoTransform transform = transformer.GetCryptoServiceProvider(bytesKey);
            CryptoStream encStream = new CryptoStream(memStreamEncryptedData, transform, CryptoStreamMode.Write);
            try
            {
                // 加密数据，并将它们写入内存流
                encStream.Write(bytesData, 0, bytesData.Length);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("将加密数据写入流时出错： \n" + ex.Message);
            }
            // 为客户端进行检索设置初始化向量和密钥

            encKey = transformer.Key;
            initVec = transformer.IV;
            encStream.FlushFinalBlock();
            encStream.Close();

            // 发送回数据
            return memStreamEncryptedData.ToArray();
            // end Encrypt
        }
    }

    /// <summary>
    /// 加密算法枚举类型
    /// </summary>
    public enum EncryptionAlgorithm
    {
        /// <summary>
        /// Des算法
        /// </summary>
        Des = 1,
        /// <summary>
        /// Rc2算法
        /// </summary>
        Rc2,
        /// <summary>
        /// Rijndael算法
        /// </summary>
        Rijndael,
        /// <summary>
        /// TripleDes算法
        /// </summary>
        TripleDes,
        /// <summary>
        /// Md5算法
        /// </summary>
        Md5
    };

    /// <summary>
    /// EncryptTransformer
    /// </summary>
    internal class EncryptTransformer
    {
        #region -- 构造函数 --
        internal EncryptTransformer(EncryptionAlgorithm algId)
        {
            // 保存正在使用的算法

            algorithmID = algId;
        }
        #endregion

        #region -- 私有变量 --
        private EncryptionAlgorithm algorithmID;
        private byte[] initVec;
        private byte[] encKey;
        #endregion

        #region -- 公共属性 --
        /// <summary>
        /// get or set iv
        /// </summary>
        internal byte[] IV
        {
            get { return initVec; }
            set { initVec = value; }
        }
        /// <summary>
        /// get or set key
        /// </summary>
        internal byte[] Key
        {
            get { return encKey; }
        }
        #endregion

        #region -- 公共方法 --
        /// <summary>
        /// this function provide to Get CryptoServiceProvider 
        /// </summary>
        /// <param name="bytesKey">key</param>
        /// <returns>CryptoServiceProvider</returns>
        internal ICryptoTransform GetCryptoServiceProvider(byte[] bytesKey)
        {
            // 选取提供程序。

            switch (algorithmID)
            {
                case EncryptionAlgorithm.Des:
                    {
                        DES des = new DESCryptoServiceProvider();
                        des.Mode = CipherMode.CBC;

                        // 查看是否提供了密钥

                        if (null == bytesKey)
                        {
                            encKey = des.Key;
                        }
                        else
                        {
                            des.Key = bytesKey;
                            encKey = des.Key;
                        }
                        // 查看客户端是否提供了初始化向量

                        if (null == initVec)
                        {
                            // 让算法创建一个

                            initVec = des.IV;
                        }
                        else
                        {
                            //不，将它提供给算法

                            des.IV = initVec;
                        }
                        return des.CreateEncryptor();
                    }
                case EncryptionAlgorithm.TripleDes:
                    {
                        TripleDES des3 = new TripleDESCryptoServiceProvider();
                        des3.Mode = CipherMode.CBC;
                        // See if a key was provided
                        if (null == bytesKey)
                        {
                            encKey = des3.Key;
                        }
                        else
                        {
                            des3.Key = bytesKey;
                            encKey = des3.Key;
                        }
                        // 查看客户端是否提供了初始化向量

                        if (null == initVec)
                        {
                            //是，让算法创建一个

                            initVec = des3.IV;
                        }
                        else
                        {
                            //不，将它提供给算法。

                            des3.IV = initVec;
                        }
                        return des3.CreateEncryptor();
                    }
                case EncryptionAlgorithm.Rc2:
                    {
                        RC2 rc2 = new RC2CryptoServiceProvider();
                        rc2.Mode = CipherMode.CBC;
                        // 测试是否提供了密钥

                        if (null == bytesKey)
                        {
                            encKey = rc2.Key;
                        }
                        else
                        {
                            rc2.Key = bytesKey;
                            encKey = rc2.Key;
                        }
                        // 查看客户端是否提供了初始化向量

                        if (null == initVec)
                        {
                            //是，让算法创建一个

                            initVec = rc2.IV;
                        }
                        else
                        {
                            //不，将它提供给算法。

                            rc2.IV = initVec;
                        }
                        return rc2.CreateEncryptor();
                    }
                case EncryptionAlgorithm.Rijndael:
                    {
                        Rijndael rijndael = new RijndaelManaged();
                        rijndael.Mode = CipherMode.CBC;
                        // 测试是否提供了密钥

                        if (null == bytesKey)
                        {
                            encKey = rijndael.Key;
                        }
                        else
                        {
                            rijndael.Key = bytesKey;
                            encKey = rijndael.Key;
                        }
                        // 查看客户端是否提供了初始化向量

                        if (null == initVec)
                        {
                            // 是，让算法创建一个

                            initVec = rijndael.IV;
                        }
                        else
                        {
                            // 不，将它提供给算法。

                            rijndael.IV = initVec;
                        }
                        return rijndael.CreateEncryptor();
                    }
                case EncryptionAlgorithm.Md5:
                    {
                        MD5 md5 = new MD5CryptoServiceProvider();
                        return md5;
                    }
                default:
                    {
                        throw new CryptographicException("算法 ID '" + algorithmID + "' 不受支持");
                    }
            }
        }
        #endregion
    }

    /// <summary>
    /// Decryptor
    /// </summary>
    internal class Decryptor
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="algId">EncryptionAlgorithm</param>
        public Decryptor(EncryptionAlgorithm algId)
        {
            transformer = new DecryptTransformer(algId);
        }

        private DecryptTransformer transformer;
        private byte[] initVec;

        public byte[] IV
        {
            set { initVec = value; }
        }

        /// <summary>
        /// decrypt
        /// </summary>
        /// <param name="bytesData">data</param>
        /// <param name="bytesKey">key</param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] bytesData, byte[] bytesKey)
        {
            // 为解密数据设置内存流
            var memStreamDecryptedData = new MemoryStream();

            // 传递初始化向量
            transformer.IV = initVec;
            var transform = transformer.GetCryptoServiceProvider(bytesKey);
            var decStream = new CryptoStream(memStreamDecryptedData, transform, CryptoStreamMode.Write);
            try
            {
                decStream.Write(bytesData, 0, bytesData.Length);
            }
            catch (Exception ex)
            {
                throw new Exception("将加密数据写入流时出错： \n" + ex.Message);
            }
            decStream.FlushFinalBlock();
            decStream.Close();
            // 发送回数据
            return memStreamDecryptedData.ToArray();
            // end Decrypt
        }
    }

    /// <summary>
    /// DecryptTransformer
    /// </summary>
    internal class DecryptTransformer
    {
        #region -- 构造函数 --
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="deCryptId">EncryptionAlgorithm</param>
        internal DecryptTransformer(EncryptionAlgorithm deCryptId)
        {
            algorithmID = deCryptId;
        }
        #endregion

        #region -- 私有变量 --
        private EncryptionAlgorithm algorithmID;
        private byte[] initVec;
        #endregion

        #region -- 公共属性 --
        /// <summary>
        /// set iv
        /// </summary>
        internal byte[] IV
        {
            set { initVec = value; }
        }
        #endregion

        #region -- 公共方法 --
        /// <summary>
        /// this function provide to GetCryptoServiceProvider
        /// </summary>
        /// <param name="bytesKey">key</param>
        /// <returns></returns>
        internal ICryptoTransform GetCryptoServiceProvider(byte[] bytesKey)
        {
            // Pick the provider.
            switch (algorithmID)
            {
                case EncryptionAlgorithm.Des:
                    {
                        DES des = new DESCryptoServiceProvider();
                        des.Mode = CipherMode.CBC;
                        des.Key = bytesKey;
                        des.IV = initVec;
                        return des.CreateDecryptor();
                    }
                case EncryptionAlgorithm.TripleDes:
                    {
                        TripleDES des3 = new TripleDESCryptoServiceProvider();
                        des3.Mode = CipherMode.CBC;
                        return des3.CreateDecryptor(bytesKey, initVec);
                    }
                case EncryptionAlgorithm.Rc2:
                    {
                        RC2 rc2 = new RC2CryptoServiceProvider();
                        rc2.Mode = CipherMode.CBC;
                        return rc2.CreateDecryptor(bytesKey, initVec);
                    }
                case EncryptionAlgorithm.Rijndael:
                    {
                        Rijndael rijndael = new RijndaelManaged();
                        rijndael.Mode = CipherMode.CBC;
                        return rijndael.CreateDecryptor(bytesKey, initVec);
                    }
                case EncryptionAlgorithm.Md5:
                    {
                        MD5 md5 = new MD5CryptoServiceProvider();
                        return md5;
                    }
                default:
                    {
                        throw new CryptographicException("算法 ID '" + algorithmID + "' 不支持");
                    }
            }
            //end GetCryptoServiceProvider
        }
        #endregion


    }
#endregion
}
