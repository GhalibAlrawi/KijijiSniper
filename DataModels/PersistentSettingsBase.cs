using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace KijijiSniper.DataModels {
    public abstract class PersistentSettingsBase<T> {
        //TODO dont remove namespaces because im high and they are declared
        public string XMLSerialize() {
            var emptyNs = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlSerializer serializer = new XmlSerializer(GetType());
            using (StringWriter myStream = new StringWriter()) {
                serializer.Serialize(myStream, this);
                myStream.Flush();
                return myStream.ToString();
            }
        }

        public static T XMLDeserialize(string xmlString) {
            if (string.IsNullOrEmpty(xmlString)) {
                throw new ArgumentNullException("xmlString");
            }
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (XmlTextReader myStream = new XmlTextReader(new StringReader(xmlString))) {
                try {
                    return (T)serializer.Deserialize(myStream);
                }
                catch (Exception ex) {
                    // The serialization error messages are cryptic at best. Give a hint at what happened
                    throw new InvalidOperationException("Failed to create object from xml string", ex);
                }
            }
        }

    }
}