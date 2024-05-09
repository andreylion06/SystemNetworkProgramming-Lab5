using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryInterfaces
{
    public interface ICharacterReplacementEncryptor
    {
        string Encrypt(int key, string plainText);
        string Decrypt(int key, string encryptedText);
    }
}
