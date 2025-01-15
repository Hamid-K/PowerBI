using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NLog.MessageTemplates;

namespace NLog.Internal
{
	// Token: 0x02000135 RID: 309
	internal sealed class PropertiesDictionary : IDictionary<object, object>, ICollection<KeyValuePair<object, object>>, IEnumerable<KeyValuePair<object, object>>, IEnumerable, IEnumerable<MessageTemplateParameter>
	{
		// Token: 0x06000F48 RID: 3912 RVA: 0x00026630 File Offset: 0x00024830
		public PropertiesDictionary(IList<MessageTemplateParameter> parameterList = null)
		{
			if (parameterList != null && parameterList.Count > 0)
			{
				MessageTemplateParameter[] array = new MessageTemplateParameter[parameterList.Count];
				for (int i = 0; i < parameterList.Count; i++)
				{
					array[i] = parameterList[i];
				}
				this.MessageProperties = array;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x00026681 File Offset: 0x00024881
		private bool IsEmpty
		{
			get
			{
				return (this._eventProperties == null || this._eventProperties.Count == 0) && (this._messageProperties == null || this._messageProperties.Count == 0);
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x000266B4 File Offset: 0x000248B4
		public IDictionary EventContext
		{
			get
			{
				IDictionary dictionary;
				if ((dictionary = this._eventContextAdapter) == null)
				{
					dictionary = (this._eventContextAdapter = new DictionaryAdapter<object, object>(this));
				}
				return dictionary;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x000266DC File Offset: 0x000248DC
		private Dictionary<object, PropertiesDictionary.PropertyValue> EventProperties
		{
			get
			{
				if (this._eventProperties == null)
				{
					if (this._messageProperties != null && this._messageProperties.Count > 0)
					{
						this._eventProperties = new Dictionary<object, PropertiesDictionary.PropertyValue>(this._messageProperties.Count);
						if (!PropertiesDictionary.InsertMessagePropertiesIntoEmptyDictionary(this._messageProperties, this._eventProperties))
						{
							this._messageProperties = PropertiesDictionary.CreateUniqueMessagePropertiesListSlow(this._messageProperties, this._eventProperties);
						}
					}
					else
					{
						this._eventProperties = new Dictionary<object, PropertiesDictionary.PropertyValue>();
					}
				}
				return this._eventProperties;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x0002675A File Offset: 0x0002495A
		// (set) Token: 0x06000F4D RID: 3917 RVA: 0x0002676B File Offset: 0x0002496B
		public IList<MessageTemplateParameter> MessageProperties
		{
			get
			{
				return this._messageProperties ?? ArrayHelper.Empty<MessageTemplateParameter>();
			}
			internal set
			{
				this._messageProperties = this.SetMessageProperties(value, this._messageProperties);
			}
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x00026780 File Offset: 0x00024980
		private IList<MessageTemplateParameter> SetMessageProperties(IList<MessageTemplateParameter> newMessageProperties, IList<MessageTemplateParameter> oldMessageProperties)
		{
			if (this._eventProperties == null && PropertiesDictionary.VerifyUniqueMessageTemplateParametersFast(newMessageProperties))
			{
				return newMessageProperties;
			}
			if (this._eventProperties == null)
			{
				this._eventProperties = new Dictionary<object, PropertiesDictionary.PropertyValue>(newMessageProperties.Count);
			}
			if (oldMessageProperties != null && this._eventProperties.Count > 0)
			{
				this.RemoveOldMessageProperties(oldMessageProperties);
			}
			if (newMessageProperties != null && (this._eventProperties.Count > 0 || !PropertiesDictionary.InsertMessagePropertiesIntoEmptyDictionary(newMessageProperties, this._eventProperties)))
			{
				return PropertiesDictionary.CreateUniqueMessagePropertiesListSlow(newMessageProperties, this._eventProperties);
			}
			return newMessageProperties;
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00026800 File Offset: 0x00024A00
		private void RemoveOldMessageProperties(IList<MessageTemplateParameter> oldMessageProperties)
		{
			for (int i = 0; i < oldMessageProperties.Count; i++)
			{
				PropertiesDictionary.PropertyValue propertyValue;
				if (this._eventProperties.TryGetValue(oldMessageProperties[i].Name, out propertyValue) && propertyValue.IsMessageProperty)
				{
					this._eventProperties.Remove(oldMessageProperties[i].Name);
				}
			}
		}

		// Token: 0x170002EF RID: 751
		public object this[object key]
		{
			get
			{
				PropertiesDictionary.PropertyValue propertyValue;
				if (!this.IsEmpty && this.EventProperties.TryGetValue(key, out propertyValue))
				{
					return propertyValue.Value;
				}
				throw new KeyNotFoundException();
			}
			set
			{
				this.EventProperties[key] = new PropertiesDictionary.PropertyValue(value, false);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x000268A6 File Offset: 0x00024AA6
		public ICollection<object> Keys
		{
			get
			{
				return this.KeyCollection;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x000268AE File Offset: 0x00024AAE
		public ICollection<object> Values
		{
			get
			{
				return this.ValueCollection;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x000268B8 File Offset: 0x00024AB8
		private PropertiesDictionary.DictionaryCollection KeyCollection
		{
			get
			{
				if (this._keyCollection != null)
				{
					return this._keyCollection;
				}
				if (this.IsEmpty)
				{
					return PropertiesDictionary.EmptyKeyCollection;
				}
				PropertiesDictionary.DictionaryCollection dictionaryCollection;
				if ((dictionaryCollection = this._keyCollection) == null)
				{
					dictionaryCollection = (this._keyCollection = new PropertiesDictionary.DictionaryCollection(this, true));
				}
				return dictionaryCollection;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x000268FC File Offset: 0x00024AFC
		private PropertiesDictionary.DictionaryCollection ValueCollection
		{
			get
			{
				if (this._valueCollection != null)
				{
					return this._valueCollection;
				}
				if (this.IsEmpty)
				{
					return PropertiesDictionary.EmptyValueCollection;
				}
				PropertiesDictionary.DictionaryCollection dictionaryCollection;
				if ((dictionaryCollection = this._valueCollection) == null)
				{
					dictionaryCollection = (this._valueCollection = new PropertiesDictionary.DictionaryCollection(this, false));
				}
				return dictionaryCollection;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x00026940 File Offset: 0x00024B40
		public int Count
		{
			get
			{
				Dictionary<object, PropertiesDictionary.PropertyValue> eventProperties = this._eventProperties;
				if (eventProperties != null)
				{
					return eventProperties.Count;
				}
				IList<MessageTemplateParameter> messageProperties = this._messageProperties;
				if (messageProperties == null)
				{
					return 0;
				}
				return messageProperties.Count;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x00026963 File Offset: 0x00024B63
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00026966 File Offset: 0x00024B66
		public void Add(object key, object value)
		{
			this.EventProperties.Add(key, new PropertiesDictionary.PropertyValue(value, false));
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0002697B File Offset: 0x00024B7B
		public void Add(KeyValuePair<object, object> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00026991 File Offset: 0x00024B91
		public void Clear()
		{
			Dictionary<object, PropertiesDictionary.PropertyValue> eventProperties = this._eventProperties;
			if (eventProperties != null)
			{
				eventProperties.Clear();
			}
			if (this._messageProperties != null)
			{
				this._messageProperties = ArrayHelper.Empty<MessageTemplateParameter>();
			}
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x000269B8 File Offset: 0x00024BB8
		public bool Contains(KeyValuePair<object, object> item)
		{
			if (!this.IsEmpty)
			{
				if (((ICollection<KeyValuePair<object, PropertiesDictionary.PropertyValue>>)this.EventProperties).Contains(new KeyValuePair<object, PropertiesDictionary.PropertyValue>(item.Key, new PropertiesDictionary.PropertyValue(item.Value, false))))
				{
					return true;
				}
				if (((ICollection<KeyValuePair<object, PropertiesDictionary.PropertyValue>>)this.EventProperties).Contains(new KeyValuePair<object, PropertiesDictionary.PropertyValue>(item.Key, new PropertiesDictionary.PropertyValue(item.Value, true))))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00026A1E File Offset: 0x00024C1E
		public bool ContainsKey(object key)
		{
			return !this.IsEmpty && this.EventProperties.ContainsKey(key);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00026A38 File Offset: 0x00024C38
		public void CopyTo(KeyValuePair<object, object>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (!this.IsEmpty)
			{
				foreach (KeyValuePair<object, object> keyValuePair in this)
				{
					array[arrayIndex++] = keyValuePair;
				}
			}
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00026AAC File Offset: 0x00024CAC
		public IEnumerator<KeyValuePair<object, object>> GetEnumerator()
		{
			if (this.IsEmpty)
			{
				return Enumerable.Empty<KeyValuePair<object, object>>().GetEnumerator();
			}
			return new PropertiesDictionary.DictionaryEnumerator(this);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00026AC7 File Offset: 0x00024CC7
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this.IsEmpty)
			{
				return ArrayHelper.Empty<KeyValuePair<object, object>>().GetEnumerator();
			}
			return new PropertiesDictionary.DictionaryEnumerator(this);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x00026AE2 File Offset: 0x00024CE2
		public bool Remove(object key)
		{
			return !this.IsEmpty && this.EventProperties.Remove(key);
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x00026AFC File Offset: 0x00024CFC
		public bool Remove(KeyValuePair<object, object> item)
		{
			if (!this.IsEmpty)
			{
				if (((IDictionary<object, PropertiesDictionary.PropertyValue>)this.EventProperties).Remove(new KeyValuePair<object, PropertiesDictionary.PropertyValue>(item.Key, new PropertiesDictionary.PropertyValue(item.Value, false))))
				{
					return true;
				}
				if (((IDictionary<object, PropertiesDictionary.PropertyValue>)this.EventProperties).Remove(new KeyValuePair<object, PropertiesDictionary.PropertyValue>(item.Key, new PropertiesDictionary.PropertyValue(item.Value, true))))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x00026B6C File Offset: 0x00024D6C
		public bool TryGetValue(object key, out object value)
		{
			PropertiesDictionary.PropertyValue propertyValue;
			if (!this.IsEmpty && this.EventProperties.TryGetValue(key, out propertyValue))
			{
				value = propertyValue.Value;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00026BA0 File Offset: 0x00024DA0
		private static bool VerifyUniqueMessageTemplateParametersFast(IList<MessageTemplateParameter> parameterList)
		{
			if (parameterList == null || parameterList.Count == 0)
			{
				return true;
			}
			if (parameterList.Count > 10)
			{
				return false;
			}
			for (int i = 0; i < parameterList.Count - 1; i++)
			{
				for (int j = i + 1; j < parameterList.Count; j++)
				{
					if (parameterList[i].Name == parameterList[j].Name)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x00026C14 File Offset: 0x00024E14
		private static bool InsertMessagePropertiesIntoEmptyDictionary(IList<MessageTemplateParameter> messageProperties, Dictionary<object, PropertiesDictionary.PropertyValue> eventProperties)
		{
			bool flag;
			try
			{
				for (int i = 0; i < messageProperties.Count; i++)
				{
					eventProperties.Add(messageProperties[i].Name, new PropertiesDictionary.PropertyValue(messageProperties[i].Value, true));
				}
				flag = true;
			}
			catch (ArgumentException)
			{
				for (int j = 0; j < messageProperties.Count; j++)
				{
					eventProperties.Remove(messageProperties[j].Name);
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00026CA0 File Offset: 0x00024EA0
		private static IList<MessageTemplateParameter> CreateUniqueMessagePropertiesListSlow(IList<MessageTemplateParameter> messageProperties, Dictionary<object, PropertiesDictionary.PropertyValue> eventProperties)
		{
			List<MessageTemplateParameter> list = null;
			for (int i = 0; i < messageProperties.Count; i++)
			{
				PropertiesDictionary.PropertyValue propertyValue;
				if (eventProperties.TryGetValue(messageProperties[i].Name, out propertyValue) && propertyValue.IsMessageProperty)
				{
					if (list == null)
					{
						list = new List<MessageTemplateParameter>(messageProperties.Count);
						for (int j = 0; j < i; j++)
						{
							list.Add(messageProperties[j]);
						}
					}
				}
				else
				{
					eventProperties[messageProperties[i].Name] = new PropertiesDictionary.PropertyValue(messageProperties[i].Value, true);
					if (list != null)
					{
						list.Add(messageProperties[i]);
					}
				}
			}
			IList<MessageTemplateParameter> list2 = list;
			return list2 ?? messageProperties;
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00026D5A File Offset: 0x00024F5A
		IEnumerator<MessageTemplateParameter> IEnumerable<MessageTemplateParameter>.GetEnumerator()
		{
			return new PropertiesDictionary.ParameterEnumerator(this);
		}

		// Token: 0x04000416 RID: 1046
		private Dictionary<object, PropertiesDictionary.PropertyValue> _eventProperties;

		// Token: 0x04000417 RID: 1047
		private IList<MessageTemplateParameter> _messageProperties;

		// Token: 0x04000418 RID: 1048
		private PropertiesDictionary.DictionaryCollection _keyCollection;

		// Token: 0x04000419 RID: 1049
		private PropertiesDictionary.DictionaryCollection _valueCollection;

		// Token: 0x0400041A RID: 1050
		private IDictionary _eventContextAdapter;

		// Token: 0x0400041B RID: 1051
		private static readonly PropertiesDictionary.DictionaryCollection EmptyKeyCollection = new PropertiesDictionary.DictionaryCollection(new PropertiesDictionary(null), true);

		// Token: 0x0400041C RID: 1052
		private static readonly PropertiesDictionary.DictionaryCollection EmptyValueCollection = new PropertiesDictionary.DictionaryCollection(new PropertiesDictionary(null), false);

		// Token: 0x02000271 RID: 625
		private struct PropertyValue
		{
			// Token: 0x06001634 RID: 5684 RVA: 0x0003A477 File Offset: 0x00038677
			public PropertyValue(object value, bool isMessageProperty)
			{
				this.Value = value;
				this.IsMessageProperty = isMessageProperty;
			}

			// Token: 0x040006B5 RID: 1717
			public readonly object Value;

			// Token: 0x040006B6 RID: 1718
			public readonly bool IsMessageProperty;
		}

		// Token: 0x02000272 RID: 626
		private abstract class DictionaryEnumeratorBase : IDisposable
		{
			// Token: 0x06001635 RID: 5685 RVA: 0x0003A487 File Offset: 0x00038687
			protected DictionaryEnumeratorBase(PropertiesDictionary dictionary)
			{
				this._dictionary = dictionary;
			}

			// Token: 0x17000418 RID: 1048
			// (get) Token: 0x06001636 RID: 5686 RVA: 0x0003A498 File Offset: 0x00038698
			protected KeyValuePair<object, object> CurrentProperty
			{
				get
				{
					if (this._messagePropertiesEnumerator != null)
					{
						MessageTemplateParameter messageTemplateParameter = this._dictionary._messageProperties[this._messagePropertiesEnumerator.Value];
						return new KeyValuePair<object, object>(messageTemplateParameter.Name, messageTemplateParameter.Value);
					}
					if (this._eventEnumeratorCreated)
					{
						KeyValuePair<object, PropertiesDictionary.PropertyValue> keyValuePair = this._eventEnumerator.Current;
						object key = keyValuePair.Key;
						keyValuePair = this._eventEnumerator.Current;
						return new KeyValuePair<object, object>(key, keyValuePair.Value.Value);
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17000419 RID: 1049
			// (get) Token: 0x06001637 RID: 5687 RVA: 0x0003A520 File Offset: 0x00038720
			protected MessageTemplateParameter CurrentParameter
			{
				get
				{
					if (this._messagePropertiesEnumerator != null)
					{
						return this._dictionary._messageProperties[this._messagePropertiesEnumerator.Value];
					}
					if (this._eventEnumeratorCreated)
					{
						KeyValuePair<object, PropertiesDictionary.PropertyValue> keyValuePair = this._eventEnumerator.Current;
						string text = XmlHelper.XmlConvertToString(keyValuePair.Key ?? string.Empty) ?? string.Empty;
						keyValuePair = this._eventEnumerator.Current;
						return new MessageTemplateParameter(text, keyValuePair.Value.Value, null, CaptureType.Unknown);
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x06001638 RID: 5688 RVA: 0x0003A5B0 File Offset: 0x000387B0
			public bool MoveNext()
			{
				if (this._messagePropertiesEnumerator != null)
				{
					if (this._messagePropertiesEnumerator.Value + 1 < this._dictionary._messageProperties.Count)
					{
						this._messagePropertiesEnumerator = this.FindNextValidMessagePropertyIndex(this._messagePropertiesEnumerator.Value + 1);
						if (this._messagePropertiesEnumerator != null)
						{
							return true;
						}
						this._messagePropertiesEnumerator = new int?(this._dictionary._eventProperties.Count - 1);
					}
					if (PropertiesDictionary.DictionaryEnumeratorBase.HasEventProperties(this._dictionary))
					{
						this._messagePropertiesEnumerator = null;
						this._eventEnumerator = this._dictionary._eventProperties.GetEnumerator();
						this._eventEnumeratorCreated = true;
						return this.MoveNextValidEventProperty();
					}
					return false;
				}
				else
				{
					if (this._eventEnumeratorCreated)
					{
						return this.MoveNextValidEventProperty();
					}
					if (PropertiesDictionary.DictionaryEnumeratorBase.HasMessageProperties(this._dictionary))
					{
						this._messagePropertiesEnumerator = this.FindNextValidMessagePropertyIndex(0);
						if (this._messagePropertiesEnumerator != null)
						{
							return true;
						}
					}
					if (PropertiesDictionary.DictionaryEnumeratorBase.HasEventProperties(this._dictionary))
					{
						this._eventEnumerator = this._dictionary._eventProperties.GetEnumerator();
						this._eventEnumeratorCreated = true;
						return this.MoveNextValidEventProperty();
					}
					return false;
				}
			}

			// Token: 0x06001639 RID: 5689 RVA: 0x0003A6DA File Offset: 0x000388DA
			private static bool HasMessageProperties(PropertiesDictionary propertiesDictionary)
			{
				return propertiesDictionary._messageProperties != null && propertiesDictionary._messageProperties.Count > 0;
			}

			// Token: 0x0600163A RID: 5690 RVA: 0x0003A6F4 File Offset: 0x000388F4
			private static bool HasEventProperties(PropertiesDictionary propertiesDictionary)
			{
				return propertiesDictionary._eventProperties != null && propertiesDictionary._eventProperties.Count > 0;
			}

			// Token: 0x0600163B RID: 5691 RVA: 0x0003A710 File Offset: 0x00038910
			private bool MoveNextValidEventProperty()
			{
				while (this._eventEnumerator.MoveNext())
				{
					KeyValuePair<object, PropertiesDictionary.PropertyValue> keyValuePair = this._eventEnumerator.Current;
					if (!keyValuePair.Value.IsMessageProperty)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600163C RID: 5692 RVA: 0x0003A74C File Offset: 0x0003894C
			private int? FindNextValidMessagePropertyIndex(int startIndex)
			{
				if (this._dictionary._eventProperties == null)
				{
					return new int?(startIndex);
				}
				for (int i = startIndex; i < this._dictionary._messageProperties.Count; i++)
				{
					PropertiesDictionary.PropertyValue propertyValue;
					if (this._dictionary._eventProperties.TryGetValue(this._dictionary._messageProperties[i].Name, out propertyValue) && propertyValue.IsMessageProperty)
					{
						return new int?(i);
					}
				}
				return null;
			}

			// Token: 0x0600163D RID: 5693 RVA: 0x0003A7CD File Offset: 0x000389CD
			public void Dispose()
			{
			}

			// Token: 0x0600163E RID: 5694 RVA: 0x0003A7CF File Offset: 0x000389CF
			public void Reset()
			{
				this._messagePropertiesEnumerator = null;
				this._eventEnumeratorCreated = false;
				this._eventEnumerator = default(Dictionary<object, PropertiesDictionary.PropertyValue>.Enumerator);
			}

			// Token: 0x040006B7 RID: 1719
			private readonly PropertiesDictionary _dictionary;

			// Token: 0x040006B8 RID: 1720
			private int? _messagePropertiesEnumerator;

			// Token: 0x040006B9 RID: 1721
			private bool _eventEnumeratorCreated;

			// Token: 0x040006BA RID: 1722
			private Dictionary<object, PropertiesDictionary.PropertyValue>.Enumerator _eventEnumerator;
		}

		// Token: 0x02000273 RID: 627
		private class ParameterEnumerator : PropertiesDictionary.DictionaryEnumeratorBase, IEnumerator<MessageTemplateParameter>, IDisposable, IEnumerator
		{
			// Token: 0x1700041A RID: 1050
			// (get) Token: 0x0600163F RID: 5695 RVA: 0x0003A7F0 File Offset: 0x000389F0
			public MessageTemplateParameter Current
			{
				get
				{
					return base.CurrentParameter;
				}
			}

			// Token: 0x1700041B RID: 1051
			// (get) Token: 0x06001640 RID: 5696 RVA: 0x0003A7F8 File Offset: 0x000389F8
			object IEnumerator.Current
			{
				get
				{
					return base.CurrentParameter;
				}
			}

			// Token: 0x06001641 RID: 5697 RVA: 0x0003A805 File Offset: 0x00038A05
			public ParameterEnumerator(PropertiesDictionary dictionary)
				: base(dictionary)
			{
			}
		}

		// Token: 0x02000274 RID: 628
		private class DictionaryEnumerator : PropertiesDictionary.DictionaryEnumeratorBase, IEnumerator<KeyValuePair<object, object>>, IDisposable, IEnumerator
		{
			// Token: 0x1700041C RID: 1052
			// (get) Token: 0x06001642 RID: 5698 RVA: 0x0003A80E File Offset: 0x00038A0E
			public KeyValuePair<object, object> Current
			{
				get
				{
					return base.CurrentProperty;
				}
			}

			// Token: 0x1700041D RID: 1053
			// (get) Token: 0x06001643 RID: 5699 RVA: 0x0003A816 File Offset: 0x00038A16
			object IEnumerator.Current
			{
				get
				{
					return base.CurrentProperty;
				}
			}

			// Token: 0x06001644 RID: 5700 RVA: 0x0003A823 File Offset: 0x00038A23
			public DictionaryEnumerator(PropertiesDictionary dictionary)
				: base(dictionary)
			{
			}
		}

		// Token: 0x02000275 RID: 629
		private class DictionaryCollection : ICollection<object>, IEnumerable<object>, IEnumerable
		{
			// Token: 0x06001645 RID: 5701 RVA: 0x0003A82C File Offset: 0x00038A2C
			public DictionaryCollection(PropertiesDictionary dictionary, bool keyCollection)
			{
				this._dictionary = dictionary;
				this._keyCollection = keyCollection;
			}

			// Token: 0x1700041E RID: 1054
			// (get) Token: 0x06001646 RID: 5702 RVA: 0x0003A842 File Offset: 0x00038A42
			public int Count
			{
				get
				{
					return this._dictionary.Count;
				}
			}

			// Token: 0x1700041F RID: 1055
			// (get) Token: 0x06001647 RID: 5703 RVA: 0x0003A84F File Offset: 0x00038A4F
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001648 RID: 5704 RVA: 0x0003A852 File Offset: 0x00038A52
			public void Add(object item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06001649 RID: 5705 RVA: 0x0003A859 File Offset: 0x00038A59
			public void Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600164A RID: 5706 RVA: 0x0003A860 File Offset: 0x00038A60
			public bool Remove(object item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600164B RID: 5707 RVA: 0x0003A868 File Offset: 0x00038A68
			public bool Contains(object item)
			{
				if (this._keyCollection)
				{
					return this._dictionary.ContainsKey(item);
				}
				if (!this._dictionary.IsEmpty)
				{
					if (this._dictionary.EventProperties.ContainsValue(new PropertiesDictionary.PropertyValue(item, false)))
					{
						return true;
					}
					if (this._dictionary.EventProperties.ContainsValue(new PropertiesDictionary.PropertyValue(item, true)))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600164C RID: 5708 RVA: 0x0003A8D0 File Offset: 0x00038AD0
			public void CopyTo(object[] array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex");
				}
				if (!this._dictionary.IsEmpty)
				{
					foreach (KeyValuePair<object, object> keyValuePair in this._dictionary)
					{
						array[arrayIndex++] = (this._keyCollection ? keyValuePair.Key : keyValuePair.Value);
					}
				}
			}

			// Token: 0x0600164D RID: 5709 RVA: 0x0003A960 File Offset: 0x00038B60
			public IEnumerator<object> GetEnumerator()
			{
				return new PropertiesDictionary.DictionaryCollection.DictionaryCollectionEnumerator(this._dictionary, this._keyCollection);
			}

			// Token: 0x0600164E RID: 5710 RVA: 0x0003A973 File Offset: 0x00038B73
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040006BB RID: 1723
			private readonly PropertiesDictionary _dictionary;

			// Token: 0x040006BC RID: 1724
			private readonly bool _keyCollection;

			// Token: 0x020002D3 RID: 723
			private class DictionaryCollectionEnumerator : PropertiesDictionary.DictionaryEnumeratorBase, IEnumerator<object>, IDisposable, IEnumerator
			{
				// Token: 0x060017AA RID: 6058 RVA: 0x0003DB2C File Offset: 0x0003BD2C
				public DictionaryCollectionEnumerator(PropertiesDictionary dictionary, bool keyCollection)
					: base(dictionary)
				{
					this._keyCollection = keyCollection;
				}

				// Token: 0x17000451 RID: 1105
				// (get) Token: 0x060017AB RID: 6059 RVA: 0x0003DB3C File Offset: 0x0003BD3C
				public object Current
				{
					get
					{
						if (!this._keyCollection)
						{
							return base.CurrentProperty.Value;
						}
						return base.CurrentProperty.Key;
					}
				}

				// Token: 0x040007C1 RID: 1985
				private readonly bool _keyCollection;
			}
		}
	}
}
