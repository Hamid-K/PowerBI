using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000029 RID: 41
	[EditorBrowsable(1)]
	public abstract class ComplexProperty : ISelfValidate, IJsonSerializable
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x000091A2 File Offset: 0x000081A2
		internal ComplexProperty()
		{
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000091B1 File Offset: 0x000081B1
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x000091B9 File Offset: 0x000081B9
		internal XmlNamespace Namespace
		{
			get
			{
				return this.xmlNamespace;
			}
			set
			{
				this.xmlNamespace = value;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000091C2 File Offset: 0x000081C2
		internal virtual void Changed()
		{
			if (this.OnChange != null)
			{
				this.OnChange(this);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000091D8 File Offset: 0x000081D8
		internal virtual void SetFieldValue<T>(ref T field, T value)
		{
			bool flag;
			if (field == null)
			{
				flag = value != null;
			}
			else
			{
				flag = !(field is IComparable) || (field as IComparable).CompareTo(value) != 0;
			}
			if (flag)
			{
				field = value;
				this.Changed();
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00009249 File Offset: 0x00008249
		internal virtual void ClearChangeLog()
		{
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000924B File Offset: 0x0000824B
		internal virtual void ReadAttributesFromXml(EwsServiceXmlReader reader)
		{
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000924D File Offset: 0x0000824D
		internal virtual void ReadTextValueFromXml(EwsServiceXmlReader reader)
		{
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000924F File Offset: 0x0000824F
		internal virtual bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			return false;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00009252 File Offset: 0x00008252
		internal virtual bool TryReadElementFromXmlToPatch(EwsServiceXmlReader reader)
		{
			return false;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009255 File Offset: 0x00008255
		internal virtual void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00009257 File Offset: 0x00008257
		internal virtual void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00009259 File Offset: 0x00008259
		internal virtual void LoadFromXml(EwsServiceXmlReader reader, XmlNamespace xmlNamespace, string xmlElementName)
		{
			this.InternalLoadFromXml(reader, xmlNamespace, xmlElementName, new Func<EwsServiceXmlReader, bool>(this.TryReadElementFromXml));
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00009271 File Offset: 0x00008271
		internal virtual void UpdateFromXml(EwsServiceXmlReader reader, XmlNamespace xmlNamespace, string xmlElementName)
		{
			this.InternalLoadFromXml(reader, xmlNamespace, xmlElementName, new Func<EwsServiceXmlReader, bool>(this.TryReadElementFromXmlToPatch));
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000928C File Offset: 0x0000828C
		private void InternalLoadFromXml(EwsServiceXmlReader reader, XmlNamespace xmlNamespace, string xmlElementName, Func<EwsServiceXmlReader, bool> readAction)
		{
			reader.EnsureCurrentNodeIsStartElement(xmlNamespace, xmlElementName);
			this.ReadAttributesFromXml(reader);
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					switch (reader.NodeType)
					{
					case 1:
						if (!readAction.Invoke(reader))
						{
							reader.SkipCurrentElement();
						}
						break;
					case 3:
						this.ReadTextValueFromXml(reader);
						break;
					}
				}
				while (!reader.IsEndElement(xmlNamespace, xmlElementName));
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000092F6 File Offset: 0x000082F6
		internal virtual void LoadFromXml(EwsServiceXmlReader reader, string xmlElementName)
		{
			this.LoadFromXml(reader, this.Namespace, xmlElementName);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00009306 File Offset: 0x00008306
		internal virtual void UpdateFromXml(EwsServiceXmlReader reader, string xmlElementName)
		{
			this.UpdateFromXml(reader, this.Namespace, xmlElementName);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00009316 File Offset: 0x00008316
		internal virtual void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000931D File Offset: 0x0000831D
		internal virtual void WriteToXml(EwsServiceXmlWriter writer, XmlNamespace xmlNamespace, string xmlElementName)
		{
			writer.WriteStartElement(xmlNamespace, xmlElementName);
			this.WriteAttributesToXml(writer);
			this.WriteElementsToXml(writer);
			writer.WriteEndElement();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000933B File Offset: 0x0000833B
		internal virtual void WriteToXml(EwsServiceXmlWriter writer, string xmlElementName)
		{
			this.WriteToXml(writer, this.Namespace, xmlElementName);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000934B File Offset: 0x0000834B
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			return this.InternalToJson(service);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009354 File Offset: 0x00008354
		internal virtual object InternalToJson(ExchangeService service)
		{
			throw new NotImplementedException();
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060001F9 RID: 505 RVA: 0x0000935B File Offset: 0x0000835B
		// (remove) Token: 0x060001FA RID: 506 RVA: 0x00009374 File Offset: 0x00008374
		internal event ComplexPropertyChangedDelegate OnChange;

		// Token: 0x060001FB RID: 507 RVA: 0x0000938D File Offset: 0x0000838D
		void ISelfValidate.Validate()
		{
			this.InternalValidate();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009395 File Offset: 0x00008395
		internal virtual void InternalValidate()
		{
		}

		// Token: 0x04000110 RID: 272
		private XmlNamespace xmlNamespace = XmlNamespace.Types;
	}
}
