using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200003E RID: 62
	public sealed class XmlNamespacePrefixCollection : KeyedCollection<string, QName>, ICollection<QName>, IEnumerable<QName>, IEnumerable, IList, ICollection, IPersistable
	{
		// Token: 0x06000277 RID: 631 RVA: 0x000098DA File Offset: 0x00007ADA
		internal XmlNamespacePrefixCollection()
		{
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000098E2 File Offset: 0x00007AE2
		public bool IsReadOnly
		{
			get
			{
				return this.m_readOnly;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000098EC File Offset: 0x00007AEC
		public void AddRange(IEnumerable<QName> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException();
			}
			this.CheckWriteable();
			foreach (QName qname in items)
			{
				base.Add(qname);
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00009944 File Offset: 0x00007B44
		protected override string GetKeyForItem(QName item)
		{
			return item.Name;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000994D File Offset: 0x00007B4D
		protected override void InsertItem(int index, QName item)
		{
			this.CheckWriteable();
			this.ValidateItem(item);
			base.InsertItem(index, item);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00009964 File Offset: 0x00007B64
		protected override void SetItem(int index, QName item)
		{
			this.CheckWriteable();
			this.ValidateItem(item);
			base.SetItem(index, item);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000997B File Offset: 0x00007B7B
		protected override void RemoveItem(int index)
		{
			this.CheckWriteable();
			base.RemoveItem(index);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000998A File Offset: 0x00007B8A
		protected override void ClearItems()
		{
			this.CheckWriteable();
			base.ClearItems();
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00009998 File Offset: 0x00007B98
		internal void AddFromReader(ModelingXmlReader xr)
		{
			if (xr.NodeType != XmlNodeType.Attribute || xr.NamespaceURI != "http://www.w3.org/2000/xmlns/")
			{
				throw new InvalidOperationException();
			}
			string text = ((xr.Prefix == "xmlns") ? xr.LocalName : xr.Prefix);
			string value = xr.Value;
			if (text.Length > 0 && !base.Contains(text))
			{
				base.Add(new QName(text, value));
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00009A0D File Offset: 0x00007C0D
		internal void MakeReadOnly()
		{
			this.m_readOnly = true;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00009A16 File Offset: 0x00007C16
		private void ValidateItem(QName item)
		{
			if (item.Name.Length == 0)
			{
				throw new ArgumentException(DevExceptionMessages.XmlNamespacePrefixCollection_DefaultNamespaceNotAllowed);
			}
			QName.Verify(item);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00009A38 File Offset: 0x00007C38
		private void CheckWriteable()
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(DevExceptionMessages.ReadOnly);
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00009A50 File Offset: 0x00007C50
		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			string[] array = new string[base.Count];
			string[] array2 = new string[base.Count];
			for (int i = 0; i < base.Count; i++)
			{
				array[i] = base[i].Name;
				array2[i] = base[i].Namespace;
			}
			writer.RegisterDeclaration(XmlNamespacePrefixCollection.Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.ReadOnly)
				{
					if (memberName != MemberName.Names)
					{
						if (memberName != MemberName.Namespaces)
						{
							throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
						}
						writer.Write(array2);
					}
					else
					{
						writer.Write(array);
					}
				}
				else
				{
					writer.Write(this.m_readOnly);
				}
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00009B34 File Offset: 0x00007D34
		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			string[] array = null;
			string[] array2 = null;
			reader.RegisterDeclaration(XmlNamespacePrefixCollection.Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.ReadOnly)
				{
					if (memberName != MemberName.Names)
					{
						if (memberName != MemberName.Namespaces)
						{
							throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
						}
						array2 = reader.ReadStringArray();
					}
					else
					{
						array = reader.ReadStringArray();
					}
				}
				else
				{
					this.m_readOnly = reader.ReadBoolean();
				}
			}
			if (array == null || array2 == null || array.Length != array2.Length)
			{
				throw new InternalModelingException("Failed to deserialize collection contents.");
			}
			bool readOnly = this.m_readOnly;
			this.m_readOnly = false;
			for (int i = 0; i < array.Length; i++)
			{
				QName qname = new QName(array[i], array2[i]);
				if (!base.Contains(qname))
				{
					base.Add(qname);
				}
			}
			this.m_readOnly = readOnly;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00009C29 File Offset: 0x00007E29
		void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00009C35 File Offset: 0x00007E35
		ObjectType IPersistable.GetObjectType()
		{
			return ObjectType.NamespacePrefixes;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00009C39 File Offset: 0x00007E39
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref XmlNamespacePrefixCollection.__declaration, XmlNamespacePrefixCollection.__declarationLock, () => new Declaration(ObjectType.NamespacePrefixes, ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.ReadOnly, Token.Boolean),
					new MemberInfo(MemberName.Names, ObjectType.PrimitiveArray, Token.String),
					new MemberInfo(MemberName.Namespaces, ObjectType.PrimitiveArray, Token.String)
				}));
			}
		}

		// Token: 0x04000143 RID: 323
		private bool m_readOnly;

		// Token: 0x04000144 RID: 324
		private static Declaration __declaration;

		// Token: 0x04000145 RID: 325
		private static readonly object __declarationLock = new object();
	}
}
