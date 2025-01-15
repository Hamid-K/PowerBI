using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.SqlServer.Types;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000537 RID: 1335
	public struct IntermediateFormatWriter
	{
		// Token: 0x060048FC RID: 18684 RVA: 0x001347D5 File Offset: 0x001329D5
		internal IntermediateFormatWriter(Stream str, int compatVersion)
		{
			this = new IntermediateFormatWriter(str, 0L, null, null, compatVersion, false);
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x001347E4 File Offset: 0x001329E4
		internal IntermediateFormatWriter(Stream str, int compatVersion, bool prohibitSerializableValues)
		{
			this = new IntermediateFormatWriter(str, 0L, null, null, compatVersion, prohibitSerializableValues);
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x001347F3 File Offset: 0x001329F3
		internal IntermediateFormatWriter(Stream str, PersistenceHelper persistenceContext, int compatVersion)
		{
			this = new IntermediateFormatWriter(str, 0L, null, persistenceContext, compatVersion, false);
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x00134802 File Offset: 0x00132A02
		internal IntermediateFormatWriter(Stream str, List<Declaration> declarations, int compatVersion)
		{
			this = new IntermediateFormatWriter(str, 0L, declarations, null, compatVersion, false);
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x00134811 File Offset: 0x00132A11
		internal IntermediateFormatWriter(Stream str, List<Declaration> declarations, int compatVersion, bool prohibitSerializableValues)
		{
			this = new IntermediateFormatWriter(str, 0L, declarations, null, compatVersion, prohibitSerializableValues);
		}

		// Token: 0x06004901 RID: 18689 RVA: 0x00134821 File Offset: 0x00132A21
		internal IntermediateFormatWriter(Stream str, List<Declaration> declarations, PersistenceHelper persistenceContext, int compatVersion)
		{
			this = new IntermediateFormatWriter(str, 0L, declarations, persistenceContext, compatVersion, false);
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x00134831 File Offset: 0x00132A31
		internal IntermediateFormatWriter(Stream str, List<Declaration> declarations, PersistenceHelper persistenceContext, int compatVersion, bool prohibitSerializableValues)
		{
			this = new IntermediateFormatWriter(str, 0L, declarations, persistenceContext, compatVersion, prohibitSerializableValues);
		}

		// Token: 0x06004903 RID: 18691 RVA: 0x00134844 File Offset: 0x00132A44
		internal IntermediateFormatWriter(Stream str, long startOffset, List<Declaration> declarations, PersistenceHelper persistenceContext, int compatVersion, bool prohibitSerializableValues)
		{
			this.m_writer = new PersistenceBinaryWriter(str);
			this.m_writtenDecls = new Dictionary<ObjectType, Declaration>(EqualityComparers.ObjectTypeComparerInstance);
			this.m_currentDeclaration = null;
			this.m_currentMemberIndex = 0;
			this.m_lastMemberInfoIndex = 0;
			this.m_currentMember = null;
			this.m_persistenceContext = persistenceContext;
			this.m_isSeekable = false;
			this.m_binaryFormatter = null;
			this.m_compatVersion = compatVersion;
			this.m_prohibitSerializableValues = prohibitSerializableValues;
			if (startOffset == 0L)
			{
				Global.Tracer.Assert(!this.m_isSeekable, "(!m_isSeekable)");
				this.Write(IntermediateFormatVersion.Current);
			}
			this.m_isSeekable = declarations != null;
			PersistenceFlags persistenceFlags = PersistenceFlags.None;
			if (this.m_isSeekable)
			{
				persistenceFlags = PersistenceFlags.Seekable;
			}
			if (this.UsesCompatVersion)
			{
				persistenceFlags |= PersistenceFlags.CompatVersioned;
			}
			if (startOffset == 0L)
			{
				this.m_writer.WriteEnum((int)persistenceFlags);
				if (this.UsesCompatVersion)
				{
					this.m_writer.Write(this.m_compatVersion);
				}
				if (this.m_isSeekable)
				{
					this.WriteDeclarations(declarations);
					return;
				}
			}
			else if (this.m_isSeekable)
			{
				this.FilterAndStoreDeclarations(declarations);
			}
		}

		// Token: 0x17001DCC RID: 7628
		// (get) Token: 0x06004904 RID: 18692 RVA: 0x0013493E File Offset: 0x00132B3E
		private bool UsesCompatVersion
		{
			get
			{
				return this.m_compatVersion != 0;
			}
		}

		// Token: 0x17001DCD RID: 7629
		// (get) Token: 0x06004905 RID: 18693 RVA: 0x00134949 File Offset: 0x00132B49
		internal MemberInfo CurrentMember
		{
			get
			{
				return this.m_currentMember;
			}
		}

		// Token: 0x17001DCE RID: 7630
		// (get) Token: 0x06004906 RID: 18694 RVA: 0x00134951 File Offset: 0x00132B51
		internal PersistenceHelper PersistenceHelper
		{
			get
			{
				return this.m_persistenceContext;
			}
		}

		// Token: 0x17001DCF RID: 7631
		// (get) Token: 0x06004907 RID: 18695 RVA: 0x00134959 File Offset: 0x00132B59
		internal bool ProhibitSerializableValues
		{
			get
			{
				return this.m_prohibitSerializableValues;
			}
		}

		// Token: 0x06004908 RID: 18696 RVA: 0x00134964 File Offset: 0x00132B64
		private void WriteDeclarations(List<Declaration> declarations)
		{
			this.m_writer.WriteListStart(ObjectType.Declaration, declarations.Count);
			for (int i = 0; i < declarations.Count; i++)
			{
				Declaration declaration = declarations[i];
				this.WriteDeclaration(declaration);
			}
		}

		// Token: 0x06004909 RID: 18697 RVA: 0x001349A8 File Offset: 0x00132BA8
		private Declaration WriteDeclaration(Declaration decl)
		{
			decl = this.FilterAndStoreDeclaration(decl);
			this.m_writer.Write(decl);
			return decl;
		}

		// Token: 0x0600490A RID: 18698 RVA: 0x001349C0 File Offset: 0x00132BC0
		private void FilterAndStoreDeclarations(List<Declaration> declarations)
		{
			for (int i = 0; i < declarations.Count; i++)
			{
				Declaration declaration = declarations[i];
				this.FilterAndStoreDeclaration(declaration);
			}
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x001349EE File Offset: 0x00132BEE
		private Declaration FilterAndStoreDeclaration(Declaration decl)
		{
			decl = decl.CreateFilteredDeclarationForWriteVersion(this.m_compatVersion);
			this.m_writtenDecls.Add(decl.ObjectType, decl);
			return decl;
		}

		// Token: 0x0600490C RID: 18700 RVA: 0x00134A14 File Offset: 0x00132C14
		internal void RegisterDeclaration(Declaration declaration)
		{
			Declaration declaration2;
			if (!this.m_writtenDecls.TryGetValue(declaration.ObjectType, out declaration2))
			{
				declaration2 = this.WriteDeclaration(declaration);
			}
			this.m_currentDeclaration = declaration2;
			this.m_lastMemberInfoIndex = this.m_currentDeclaration.MemberInfoList.Count - 1;
			this.m_currentMemberIndex = -1;
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x00134A64 File Offset: 0x00132C64
		internal bool NextMember()
		{
			if (this.m_currentMemberIndex < this.m_lastMemberInfoIndex)
			{
				this.m_currentMemberIndex++;
				this.m_currentMember = this.m_currentDeclaration.MemberInfoList[this.m_currentMemberIndex];
				return true;
			}
			return false;
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x00134AA1 File Offset: 0x00132CA1
		internal void Write(IPersistable persistableObj)
		{
			this.Write(persistableObj, true);
		}

		// Token: 0x0600490F RID: 18703 RVA: 0x00134AAB File Offset: 0x00132CAB
		private void Write(IPersistable persistableObj, bool verify)
		{
			if (persistableObj != null)
			{
				this.m_writer.Write(persistableObj.GetObjectType());
				persistableObj.Serialize(this);
				return;
			}
			this.m_writer.WriteNull();
		}

		// Token: 0x06004910 RID: 18704 RVA: 0x00134ADC File Offset: 0x00132CDC
		internal void WriteNameObjectCollection(INameObjectCollection collection)
		{
			if (collection == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, collection.Count);
			for (int i = 0; i < collection.Count; i++)
			{
				this.m_writer.Write(collection.GetKey(i));
				this.Write(collection.GetValue(i));
			}
		}

		// Token: 0x06004911 RID: 18705 RVA: 0x00134B44 File Offset: 0x00132D44
		internal void WriteStringRIFObjectDictionary<TVal>(Dictionary<string, TVal> dictionary) where TVal : IPersistable
		{
			if (dictionary == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, dictionary.Count);
			foreach (KeyValuePair<string, TVal> keyValuePair in dictionary)
			{
				this.m_writer.Write(keyValuePair.Key);
				this.Write(keyValuePair.Value);
			}
		}

		// Token: 0x06004912 RID: 18706 RVA: 0x00134BDC File Offset: 0x00132DDC
		internal void WriteStringListOfStringDictionary(Dictionary<string, List<string>> dictionary)
		{
			if (dictionary == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, dictionary.Count);
			foreach (KeyValuePair<string, List<string>> keyValuePair in dictionary)
			{
				this.m_writer.Write(keyValuePair.Key);
				this.WriteListOfPrimitives<string>(keyValuePair.Value, false);
			}
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x00134C70 File Offset: 0x00132E70
		internal void WriteInt32RIFObjectDictionary<TVal>(Dictionary<int, TVal> dictionary) where TVal : IPersistable
		{
			if (dictionary == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, dictionary.Count);
			foreach (KeyValuePair<int, TVal> keyValuePair in dictionary)
			{
				this.m_writer.Write(keyValuePair.Key);
				this.Write(keyValuePair.Value);
			}
		}

		// Token: 0x06004914 RID: 18708 RVA: 0x00134D08 File Offset: 0x00132F08
		internal void WriteInt32RIFObjectDictionary<TVal>(IDictionary dictionary) where TVal : IPersistable
		{
			if (dictionary == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, dictionary.Count);
			foreach (object obj in dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.m_writer.Write((int)dictionaryEntry.Key);
				this.Write((TVal)((object)dictionaryEntry.Value));
			}
		}

		// Token: 0x06004915 RID: 18709 RVA: 0x00134DB0 File Offset: 0x00132FB0
		internal void WriteStringRIFObjectHashtable(Hashtable hashtable)
		{
			if (hashtable == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, hashtable.Count);
			foreach (object obj in hashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.m_writer.Write((string)dictionaryEntry.Key);
				this.Write((IPersistable)dictionaryEntry.Value);
			}
		}

		// Token: 0x06004916 RID: 18710 RVA: 0x00134E54 File Offset: 0x00133054
		internal void WriteInt32PrimitiveListHashtable<T>(Hashtable hashtable)
		{
			if (hashtable == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, hashtable.Count);
			foreach (object obj in hashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.m_writer.Write((int)dictionaryEntry.Key);
				this.WriteListOfPrimitives<T>((List<T>)dictionaryEntry.Value, false);
			}
		}

		// Token: 0x06004917 RID: 18711 RVA: 0x00134EF8 File Offset: 0x001330F8
		internal void WriteStringObjectHashtable(IDictionary dictionary)
		{
			if (dictionary == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, dictionary.Count);
			foreach (object obj in dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.m_writer.Write((string)dictionaryEntry.Key);
				this.Write(dictionaryEntry.Value);
			}
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x00134F94 File Offset: 0x00133194
		internal void WriteStringRIFObjectHashtable(IDictionary dictionary)
		{
			if (dictionary == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, dictionary.Count);
			foreach (object obj in dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.m_writer.Write((string)dictionaryEntry.Key);
				this.Write((IPersistable)dictionaryEntry.Value);
			}
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x00135038 File Offset: 0x00133238
		internal void WriteStringInt32Hashtable(IDictionary dictionary)
		{
			if (dictionary == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, dictionary.Count);
			foreach (object obj in dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.m_writer.Write((string)dictionaryEntry.Key);
				this.m_writer.Write((int)dictionaryEntry.Value);
			}
		}

		// Token: 0x0600491A RID: 18714 RVA: 0x001350E0 File Offset: 0x001332E0
		internal void WriteStringStringHashtable(IDictionary dictionary)
		{
			if (dictionary == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, dictionary.Count);
			foreach (object obj in dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.m_writer.Write((string)dictionaryEntry.Key);
				this.m_writer.Write((string)dictionaryEntry.Value);
			}
		}

		// Token: 0x0600491B RID: 18715 RVA: 0x00135188 File Offset: 0x00133388
		internal void WriteObjectHashtableHashtable(Hashtable hashtable)
		{
			if (hashtable == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, hashtable.Count);
			foreach (object obj in hashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.Write(dictionaryEntry.Key);
				this.WriteVariantVariantHashtable((Hashtable)dictionaryEntry.Value);
			}
		}

		// Token: 0x0600491C RID: 18716 RVA: 0x00135220 File Offset: 0x00133420
		internal void WriteNLevelVariantHashtable(Hashtable hashtable)
		{
			if (hashtable == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, hashtable.Count);
			foreach (object obj in hashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.Write(dictionaryEntry.Key);
				Hashtable hashtable2 = dictionaryEntry.Value as Hashtable;
				if (hashtable2 != null)
				{
					this.m_writer.Write(Token.Hashtable);
					this.WriteNLevelVariantHashtable(hashtable2);
				}
				else
				{
					this.m_writer.Write(Token.Object);
					this.Write(dictionaryEntry.Value, false, true);
				}
			}
		}

		// Token: 0x0600491D RID: 18717 RVA: 0x001352EC File Offset: 0x001334EC
		internal void WriteRIFObjectStringHashtable(IDictionary hashtable)
		{
			if (hashtable == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, hashtable.Count);
			foreach (object obj in hashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.Write((IPersistable)dictionaryEntry.Key);
				this.m_writer.Write((string)dictionaryEntry.Value);
			}
		}

		// Token: 0x0600491E RID: 18718 RVA: 0x00135390 File Offset: 0x00133590
		internal void WriteVariantVariantHashtable(Hashtable hashtable)
		{
			if (hashtable == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, hashtable.Count);
			foreach (object obj in hashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.Write(dictionaryEntry.Key);
				this.Write(dictionaryEntry.Value);
			}
		}

		// Token: 0x0600491F RID: 18719 RVA: 0x00135424 File Offset: 0x00133624
		internal void WriteVariantListVariantDictionary(Dictionary<List<object>, object> dictionary)
		{
			if (dictionary == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, dictionary.Count);
			foreach (KeyValuePair<List<object>, object> keyValuePair in dictionary)
			{
				this.WriteListOfVariant(keyValuePair.Key);
				this.Write(keyValuePair.Value);
			}
		}

		// Token: 0x06004920 RID: 18720 RVA: 0x001354B0 File Offset: 0x001336B0
		internal void WriteStringVariantListDictionary(Dictionary<string, List<object>> dictionary)
		{
			if (dictionary == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, dictionary.Count);
			foreach (KeyValuePair<string, List<object>> keyValuePair in dictionary)
			{
				this.Write(keyValuePair.Key);
				this.WriteListOfVariant(keyValuePair.Value);
			}
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x0013553C File Offset: 0x0013373C
		internal void WriteStringBoolArrayDictionary(Dictionary<string, bool[]> dictionary)
		{
			if (dictionary == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, dictionary.Count);
			foreach (KeyValuePair<string, bool[]> keyValuePair in dictionary)
			{
				this.m_writer.Write(keyValuePair.Key);
				this.m_writer.Write(keyValuePair.Value);
			}
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x001355D4 File Offset: 0x001337D4
		internal void WriteInt32StringHashtable(Hashtable hashtable)
		{
			if (hashtable == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, hashtable.Count);
			foreach (object obj in hashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.m_writer.Write((int)dictionaryEntry.Key);
				this.m_writer.Write((string)dictionaryEntry.Value);
			}
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x0013567C File Offset: 0x0013387C
		internal void WriteByteVariantHashtable(IDictionary dictionary)
		{
			if (dictionary == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, dictionary.Count);
			foreach (object obj in dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.m_writer.Write((byte)dictionaryEntry.Key);
				this.Write(dictionaryEntry.Value);
			}
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x00135718 File Offset: 0x00133918
		internal void WriteVariantRifObjectDictionary(IDictionary dictionary)
		{
			if (dictionary == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, dictionary.Count);
			foreach (object obj in dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.Write(dictionaryEntry.Key);
				this.Write((IPersistable)dictionaryEntry.Value);
			}
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x001357B0 File Offset: 0x001339B0
		internal void WriteVariantListOfRifObjectDictionary(IDictionary dictionary)
		{
			if (dictionary == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, dictionary.Count);
			foreach (object obj in dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.Write(dictionaryEntry.Key);
				this.Write((IPersistable)dictionaryEntry.Value);
			}
		}

		// Token: 0x06004926 RID: 18726 RVA: 0x00135848 File Offset: 0x00133A48
		internal void Int32SerializableDictionary(Dictionary<int, object> dictionary)
		{
			if (dictionary == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteDictionaryStart(this.m_currentMember.ObjectType, dictionary.Count);
			foreach (KeyValuePair<int, object> keyValuePair in dictionary)
			{
				this.m_writer.Write(keyValuePair.Key);
				this.WriteSerializable(keyValuePair.Value);
			}
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x001358DC File Offset: 0x00133ADC
		internal void WriteListOfReferences(IList rifList)
		{
			if (rifList == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteListStart(this.m_currentMember.ObjectType, rifList.Count);
			for (int i = 0; i < rifList.Count; i++)
			{
				this.WriteReferenceInList((IReferenceable)rifList[i]);
			}
		}

		// Token: 0x06004928 RID: 18728 RVA: 0x00135938 File Offset: 0x00133B38
		internal void WriteListOfGlobalReferences(IList rifList)
		{
			if (rifList == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteListStart(this.m_currentMember.ObjectType, rifList.Count);
			for (int i = 0; i < rifList.Count; i++)
			{
				this.WriteGlobalReferenceInList((IGloballyReferenceable)rifList[i]);
			}
		}

		// Token: 0x06004929 RID: 18729 RVA: 0x00135993 File Offset: 0x00133B93
		internal void Write<T>(List<T> rifList) where T : IPersistable
		{
			this.WriteRIFList<T>(rifList);
		}

		// Token: 0x0600492A RID: 18730 RVA: 0x0013599C File Offset: 0x00133B9C
		internal void WriteRIFList<T>(IList<T> rifList) where T : IPersistable
		{
			if (rifList == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteListStart(this.m_currentMember.ObjectType, rifList.Count);
			for (int i = 0; i < rifList.Count; i++)
			{
				this.Write(rifList[i]);
			}
		}

		// Token: 0x0600492B RID: 18731 RVA: 0x001359F8 File Offset: 0x00133BF8
		internal void Write(ArrayList rifObjectList)
		{
			if (rifObjectList == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteListStart(this.m_currentMember.ObjectType, rifObjectList.Count);
			for (int i = 0; i < rifObjectList.Count; i++)
			{
				this.Write((IPersistable)rifObjectList[i]);
			}
		}

		// Token: 0x0600492C RID: 18732 RVA: 0x00135A54 File Offset: 0x00133C54
		internal void Write<T>(List<List<T>> rifObjectLists) where T : IPersistable
		{
			if (rifObjectLists == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteListStart(this.m_currentMember.ObjectType, rifObjectLists.Count);
			for (int i = 0; i < rifObjectLists.Count; i++)
			{
				this.Write<T>(rifObjectLists[i]);
			}
		}

		// Token: 0x0600492D RID: 18733 RVA: 0x00135AAC File Offset: 0x00133CAC
		internal void Write<T>(List<T[]> rifObjectArrays) where T : IPersistable
		{
			if (rifObjectArrays == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteListStart(this.m_currentMember.ObjectType, rifObjectArrays.Count);
			for (int i = 0; i < rifObjectArrays.Count; i++)
			{
				this.Write(rifObjectArrays[i]);
			}
		}

		// Token: 0x0600492E RID: 18734 RVA: 0x00135B04 File Offset: 0x00133D04
		internal void WriteListOfVariant(IList list)
		{
			if (list == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteListStart(ObjectType.VariantList, list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				this.Write(list[i], false, true);
			}
		}

		// Token: 0x0600492F RID: 18735 RVA: 0x00135B58 File Offset: 0x00133D58
		internal void WriteArrayListOfPrimitives(ArrayList list)
		{
			if (list == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteListStart(ObjectType.PrimitiveList, list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				this.Write(list[i], false, true);
			}
		}

		// Token: 0x06004930 RID: 18736 RVA: 0x00135BA7 File Offset: 0x00133DA7
		internal void WriteListOfPrimitives<T>(List<T> list)
		{
			this.WriteListOfPrimitives<T>(list, true);
		}

		// Token: 0x06004931 RID: 18737 RVA: 0x00135BB4 File Offset: 0x00133DB4
		private void WriteListOfPrimitives<T>(List<T> list, bool verify)
		{
			if (list == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteListStart(ObjectType.PrimitiveList, list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				this.Write(list[i], false, true);
			}
		}

		// Token: 0x06004932 RID: 18738 RVA: 0x00135C08 File Offset: 0x00133E08
		internal void WriteArrayOfListsOfPrimitives<T>(List<T>[] arrayOfLists)
		{
			this.WriteArrayOfListsOfPrimitives<T>(arrayOfLists, true);
		}

		// Token: 0x06004933 RID: 18739 RVA: 0x00135C14 File Offset: 0x00133E14
		private void WriteArrayOfListsOfPrimitives<T>(List<T>[] arrayOfLists, bool validate)
		{
			if (arrayOfLists == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteArrayStart(ObjectType.PrimitiveArray, arrayOfLists.Length);
			for (int i = 0; i < arrayOfLists.Length; i++)
			{
				this.WriteListOfPrimitives<T>(arrayOfLists[i]);
			}
		}

		// Token: 0x06004934 RID: 18740 RVA: 0x00135C58 File Offset: 0x00133E58
		internal void WriteListOfArrayOfListsOfPrimitives<T>(List<List<T>[]> outerList)
		{
			if (outerList == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteListStart(ObjectType.PrimitiveList, outerList.Count);
			for (int i = 0; i < outerList.Count; i++)
			{
				this.WriteArrayOfListsOfPrimitives<T>(outerList[i], false);
			}
		}

		// Token: 0x06004935 RID: 18741 RVA: 0x00135CA8 File Offset: 0x00133EA8
		internal void Write<T>(List<T>[] rifObjectListArray) where T : IPersistable
		{
			if (rifObjectListArray == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteArrayStart(ObjectType.RIFObjectArray, rifObjectListArray.Length);
			for (int i = 0; i < rifObjectListArray.Length; i++)
			{
				this.Write<T>(rifObjectListArray[i]);
			}
		}

		// Token: 0x06004936 RID: 18742 RVA: 0x00135CEC File Offset: 0x00133EEC
		internal void Write(string[] strings)
		{
			if (strings == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteArrayStart(ObjectType.PrimitiveArray, strings.Length);
			for (int i = 0; i < strings.Length; i++)
			{
				this.Write(strings[i]);
			}
		}

		// Token: 0x06004937 RID: 18743 RVA: 0x00135D30 File Offset: 0x00133F30
		internal void Write(object[] array)
		{
			if (array == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteArrayStart(ObjectType.PrimitiveArray, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				this.Write(array[i]);
			}
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x00135D74 File Offset: 0x00133F74
		internal void WriteVariantOrPersistableArray(object[] array)
		{
			if (array == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteArrayStart(ObjectType.PrimitiveArray, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				this.WriteVariantOrPersistable(array[i]);
			}
		}

		// Token: 0x06004939 RID: 18745 RVA: 0x00135DB8 File Offset: 0x00133FB8
		internal void WriteSerializableArray(object[] array)
		{
			if (array == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteArrayStart(ObjectType.SerializableArray, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				this.WriteSerializable(array[i]);
			}
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x00135E00 File Offset: 0x00134000
		internal void Write(IPersistable[] array)
		{
			if (array == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			this.m_writer.WriteArrayStart(ObjectType.RIFObjectArray, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				this.Write(array[i]);
			}
		}

		// Token: 0x0600493B RID: 18747 RVA: 0x00135E44 File Offset: 0x00134044
		internal void Write(IPersistable[,][] array)
		{
			if (array == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			int length = array.GetLength(0);
			int length2 = array.GetLength(1);
			this.m_writer.Write2DArrayStart(ObjectType.Array2D, length, length2);
			for (int i = 0; i < length; i++)
			{
				for (int j = 0; j < length2; j++)
				{
					this.Write(array[i, j]);
				}
			}
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x00135EA8 File Offset: 0x001340A8
		internal void Write(IPersistable[,] array)
		{
			if (array == null)
			{
				this.m_writer.WriteNull();
				return;
			}
			int length = array.GetLength(0);
			int length2 = array.GetLength(1);
			this.m_writer.Write2DArrayStart(ObjectType.Array2D, length, length2);
			for (int i = 0; i < length; i++)
			{
				for (int j = 0; j < length2; j++)
				{
					this.Write(array[i, j]);
				}
			}
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x00135F09 File Offset: 0x00134109
		internal void Write(float[] array)
		{
			this.m_writer.Write(array);
		}

		// Token: 0x0600493E RID: 18750 RVA: 0x00135F17 File Offset: 0x00134117
		internal void Write(int[] array)
		{
			this.m_writer.Write(array);
		}

		// Token: 0x0600493F RID: 18751 RVA: 0x00135F25 File Offset: 0x00134125
		internal void Write(long[] array)
		{
			this.m_writer.Write(array);
		}

		// Token: 0x06004940 RID: 18752 RVA: 0x00135F33 File Offset: 0x00134133
		internal void Write(char[] array)
		{
			this.m_writer.Write(array);
		}

		// Token: 0x06004941 RID: 18753 RVA: 0x00135F41 File Offset: 0x00134141
		internal void Write(byte[] array)
		{
			this.m_writer.Write(array);
		}

		// Token: 0x06004942 RID: 18754 RVA: 0x00135F4F File Offset: 0x0013414F
		internal void Write(bool[] array)
		{
			this.m_writer.Write(array);
		}

		// Token: 0x06004943 RID: 18755 RVA: 0x00135F5D File Offset: 0x0013415D
		internal void Write(double[] array)
		{
			this.m_writer.Write(array);
		}

		// Token: 0x06004944 RID: 18756 RVA: 0x00135F6B File Offset: 0x0013416B
		internal void Write(DateTime dateTime)
		{
			this.m_writer.Write(dateTime, this.m_currentMember.Token);
		}

		// Token: 0x06004945 RID: 18757 RVA: 0x00135F84 File Offset: 0x00134184
		internal void Write(DateTimeOffset dateTimeOffset)
		{
			this.m_writer.Write(dateTimeOffset);
		}

		// Token: 0x06004946 RID: 18758 RVA: 0x00135F92 File Offset: 0x00134192
		internal void Write(TimeSpan timeSpan)
		{
			this.m_writer.Write(timeSpan);
		}

		// Token: 0x06004947 RID: 18759 RVA: 0x00135FA0 File Offset: 0x001341A0
		internal void Write(Guid guid)
		{
			this.m_writer.Write(guid);
		}

		// Token: 0x06004948 RID: 18760 RVA: 0x00135FAE File Offset: 0x001341AE
		internal void Write(string value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x06004949 RID: 18761 RVA: 0x00135FBC File Offset: 0x001341BC
		internal void Write(bool value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x0600494A RID: 18762 RVA: 0x00135FCA File Offset: 0x001341CA
		internal void Write(short value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x0600494B RID: 18763 RVA: 0x00135FD8 File Offset: 0x001341D8
		internal void Write(int value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x0600494C RID: 18764 RVA: 0x00135FE6 File Offset: 0x001341E6
		internal void Write(long value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x00135FF4 File Offset: 0x001341F4
		internal void Write(ushort value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x0600494E RID: 18766 RVA: 0x00136002 File Offset: 0x00134202
		internal void Write(uint value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x0600494F RID: 18767 RVA: 0x00136010 File Offset: 0x00134210
		internal void Write(ulong value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x06004950 RID: 18768 RVA: 0x0013601E File Offset: 0x0013421E
		internal void Write(char value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x06004951 RID: 18769 RVA: 0x0013602C File Offset: 0x0013422C
		internal void Write(byte value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x06004952 RID: 18770 RVA: 0x0013603A File Offset: 0x0013423A
		internal void Write(sbyte value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x06004953 RID: 18771 RVA: 0x00136048 File Offset: 0x00134248
		internal void Write(float value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x06004954 RID: 18772 RVA: 0x00136056 File Offset: 0x00134256
		internal void Write(double value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x06004955 RID: 18773 RVA: 0x00136064 File Offset: 0x00134264
		internal void Write(decimal value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x06004956 RID: 18774 RVA: 0x00136072 File Offset: 0x00134272
		internal void Write7BitEncodedInt(int value)
		{
			this.m_writer.WriteEnum(value);
		}

		// Token: 0x06004957 RID: 18775 RVA: 0x00136080 File Offset: 0x00134280
		internal void WriteEnum(int value)
		{
			this.m_writer.WriteEnum(value);
		}

		// Token: 0x06004958 RID: 18776 RVA: 0x0013608E File Offset: 0x0013428E
		internal void WriteNull()
		{
			this.m_writer.WriteNull();
		}

		// Token: 0x06004959 RID: 18777 RVA: 0x0013609B File Offset: 0x0013429B
		internal void Write(CultureInfo threadCulture)
		{
			if (threadCulture != null)
			{
				this.m_writer.Write(threadCulture.LCID);
				return;
			}
			this.m_writer.Write(-1);
		}

		// Token: 0x0600495A RID: 18778 RVA: 0x001360BE File Offset: 0x001342BE
		private void WriteReferenceInList(IReferenceable referenceableItem)
		{
			this.WriteReferenceID((referenceableItem != null) ? referenceableItem.ID : (-2));
		}

		// Token: 0x0600495B RID: 18779 RVA: 0x001360D3 File Offset: 0x001342D3
		internal void WriteReference(IReferenceable referenceableItem)
		{
			this.WriteReferenceID((referenceableItem != null) ? referenceableItem.ID : (-1));
		}

		// Token: 0x0600495C RID: 18780 RVA: 0x001360E7 File Offset: 0x001342E7
		internal void WriteReferenceID(int referenceID)
		{
			if (referenceID == -1)
			{
				this.WriteNull();
				return;
			}
			this.m_writer.Write(this.m_currentMember.ObjectType);
			this.m_writer.Write(referenceID);
		}

		// Token: 0x0600495D RID: 18781 RVA: 0x00136116 File Offset: 0x00134316
		private void WriteGlobalReferenceInList(IGloballyReferenceable globalReference)
		{
			this.WriteGlobalReferenceID((globalReference != null) ? globalReference.GlobalID : (-2));
		}

		// Token: 0x0600495E RID: 18782 RVA: 0x0013612B File Offset: 0x0013432B
		internal void WriteGlobalReference(IGloballyReferenceable globalReference)
		{
			this.WriteGlobalReferenceID((globalReference != null) ? globalReference.GlobalID : (-1));
		}

		// Token: 0x0600495F RID: 18783 RVA: 0x0013613F File Offset: 0x0013433F
		internal void WriteGlobalReferenceID(int globalReferenceID)
		{
			if (globalReferenceID == -1)
			{
				this.WriteNull();
				return;
			}
			this.m_writer.Write(this.m_currentMember.ObjectType);
			this.m_writer.Write(globalReferenceID);
		}

		// Token: 0x06004960 RID: 18784 RVA: 0x0013616E File Offset: 0x0013436E
		internal void Write(ObjectType type)
		{
			this.m_writer.Write(type);
		}

		// Token: 0x06004961 RID: 18785 RVA: 0x0013617C File Offset: 0x0013437C
		internal bool CanWrite(object obj)
		{
			if (obj == null)
			{
				return true;
			}
			switch (Type.GetTypeCode(obj.GetType()))
			{
			case TypeCode.Empty:
			case TypeCode.DBNull:
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
			case TypeCode.DateTime:
			case TypeCode.String:
				return true;
			}
			return obj is IPersistable || obj is DateTimeOffset || obj is TimeSpan || obj is Guid || obj is byte[] || (obj is SqlGeography || obj is SqlGeometry);
		}

		// Token: 0x06004962 RID: 18786 RVA: 0x0013622D File Offset: 0x0013442D
		internal void WriteSerializable(object obj)
		{
			if (!this.TryWriteSerializable(obj))
			{
				this.m_writer.Write(Token.Null);
			}
		}

		// Token: 0x06004963 RID: 18787 RVA: 0x00136244 File Offset: 0x00134444
		internal bool TryWriteSerializable(object obj)
		{
			if (!this.TryWrite(obj))
			{
				long position = this.m_writer.BaseStream.Position;
				try
				{
					if (this.ProhibitSerializableValues || (!(obj is ISerializable) && (obj.GetType().Attributes & TypeAttributes.Serializable) == TypeAttributes.NotPublic))
					{
						return false;
					}
					if (this.m_binaryFormatter == null)
					{
						this.m_binaryFormatter = new BinaryFormatter();
					}
					this.m_writer.Write(Token.Serializable);
					this.m_binaryFormatter.Serialize(this.m_writer.BaseStream, obj);
				}
				catch (RSException)
				{
					throw;
				}
				catch (Exception ex)
				{
					this.m_writer.BaseStream.Position = position;
					Global.Tracer.Trace(TraceLevel.Warning, "Error occurred during serialization: " + ex.Message);
					return false;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06004964 RID: 18788 RVA: 0x00136328 File Offset: 0x00134528
		internal void WriteVariantOrPersistable(object obj)
		{
			IPersistable persistable = obj as IPersistable;
			if (persistable != null)
			{
				this.m_writer.Write(Token.Object);
				this.Write(persistable, false);
				return;
			}
			this.Write(obj);
		}

		// Token: 0x06004965 RID: 18789 RVA: 0x0013635B File Offset: 0x0013455B
		internal void Write(object obj)
		{
			this.Write(obj, true, true);
		}

		// Token: 0x06004966 RID: 18790 RVA: 0x00136367 File Offset: 0x00134567
		internal bool TryWrite(object obj)
		{
			return this.Write(obj, true, false);
		}

		// Token: 0x06004967 RID: 18791 RVA: 0x00136374 File Offset: 0x00134574
		private bool Write(object obj, bool verify, bool assertOnInvalidType)
		{
			if (obj == null || obj == DBNull.Value)
			{
				this.m_writer.Write(Token.Null);
			}
			else
			{
				switch (Type.GetTypeCode(obj.GetType()))
				{
				case TypeCode.Empty:
				case TypeCode.DBNull:
					this.m_writer.Write(Token.Null);
					return true;
				case TypeCode.Boolean:
					this.m_writer.Write(Token.Boolean);
					this.m_writer.Write((bool)obj);
					return true;
				case TypeCode.Char:
					this.m_writer.Write(Token.Char);
					this.m_writer.Write((char)obj);
					return true;
				case TypeCode.SByte:
					this.m_writer.Write(Token.SByte);
					this.m_writer.Write((sbyte)obj);
					return true;
				case TypeCode.Byte:
					this.m_writer.Write(Token.Byte);
					this.m_writer.Write((byte)obj);
					return true;
				case TypeCode.Int16:
					this.m_writer.Write(Token.Int16);
					this.m_writer.Write((short)obj);
					return true;
				case TypeCode.UInt16:
					this.m_writer.Write(Token.UInt16);
					this.m_writer.Write((ushort)obj);
					return true;
				case TypeCode.Int32:
					this.m_writer.Write(Token.Int32);
					this.m_writer.Write((int)obj);
					return true;
				case TypeCode.UInt32:
					this.m_writer.Write(Token.UInt32);
					this.m_writer.Write((uint)obj);
					return true;
				case TypeCode.Int64:
					this.m_writer.Write(Token.Int64);
					this.m_writer.Write((long)obj);
					return true;
				case TypeCode.UInt64:
					this.m_writer.Write(Token.UInt64);
					this.m_writer.Write((ulong)obj);
					return true;
				case TypeCode.Single:
					this.m_writer.Write(Token.Single);
					this.m_writer.Write((float)obj);
					return true;
				case TypeCode.Double:
					this.m_writer.Write(Token.Double);
					this.m_writer.Write((double)obj);
					return true;
				case TypeCode.Decimal:
					this.m_writer.Write(Token.Decimal);
					this.m_writer.Write((decimal)obj);
					return true;
				case TypeCode.DateTime:
				{
					DateTime dateTime = (DateTime)obj;
					Token token = this.m_currentMember.Token;
					if (token == Token.Object || token == Token.Serializable)
					{
						token = ((dateTime.Kind == DateTimeKind.Unspecified) ? Token.DateTime : Token.DateTimeWithKind);
					}
					this.m_writer.Write(token);
					this.m_writer.Write(dateTime, token);
					return true;
				}
				case TypeCode.String:
					this.m_writer.Write(Token.String);
					this.m_writer.Write((string)obj, false);
					return true;
				}
				if (obj is TimeSpan)
				{
					this.m_writer.Write(Token.TimeSpan);
					this.m_writer.Write((TimeSpan)obj);
				}
				else if (obj is DateTimeOffset)
				{
					this.m_writer.Write(Token.DateTimeOffset);
					this.m_writer.Write((DateTimeOffset)obj);
				}
				else if (obj is Guid)
				{
					this.m_writer.Write(Token.Guid);
					this.m_writer.Write((Guid)obj);
				}
				else if (obj is Enum)
				{
					Global.Tracer.Assert(false, "You must call WriteEnum for enums");
				}
				else if (obj is byte[])
				{
					this.m_writer.Write(Token.ByteArray);
					this.m_writer.Write((byte[])obj);
				}
				else if (obj is SqlGeography)
				{
					this.m_writer.Write(Token.SqlGeography);
					this.m_writer.Write((SqlGeography)obj);
				}
				else
				{
					if (!(obj is SqlGeometry))
					{
						if (assertOnInvalidType)
						{
							RSTrace tracer = Global.Tracer;
							bool flag = false;
							string text = "Unsupported object type: ";
							Type type = obj.GetType();
							tracer.Assert(flag, text + ((type != null) ? type.ToString() : null));
						}
						return false;
					}
					this.m_writer.Write(Token.SqlGeometry);
					this.m_writer.Write((SqlGeometry)obj);
				}
			}
			return true;
		}

		// Token: 0x04002062 RID: 8290
		private int m_currentMemberIndex;

		// Token: 0x04002063 RID: 8291
		private Declaration m_currentDeclaration;

		// Token: 0x04002064 RID: 8292
		private Dictionary<ObjectType, Declaration> m_writtenDecls;

		// Token: 0x04002065 RID: 8293
		private PersistenceBinaryWriter m_writer;

		// Token: 0x04002066 RID: 8294
		private PersistenceHelper m_persistenceContext;

		// Token: 0x04002067 RID: 8295
		private bool m_isSeekable;

		// Token: 0x04002068 RID: 8296
		private int m_lastMemberInfoIndex;

		// Token: 0x04002069 RID: 8297
		private MemberInfo m_currentMember;

		// Token: 0x0400206A RID: 8298
		private readonly bool m_prohibitSerializableValues;

		// Token: 0x0400206B RID: 8299
		private int m_compatVersion;

		// Token: 0x0400206C RID: 8300
		private BinaryFormatter m_binaryFormatter;
	}
}
