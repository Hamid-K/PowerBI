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
	// Token: 0x0200001C RID: 28
	public struct IntermediateFormatWriter
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00005C01 File Offset: 0x00003E01
		internal IntermediateFormatWriter(Stream str, int compatVersion)
		{
			this = new IntermediateFormatWriter(str, 0L, null, null, compatVersion, false);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005C10 File Offset: 0x00003E10
		internal IntermediateFormatWriter(Stream str, int compatVersion, bool prohibitSerializableValues)
		{
			this = new IntermediateFormatWriter(str, 0L, null, null, compatVersion, prohibitSerializableValues);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005C1F File Offset: 0x00003E1F
		internal IntermediateFormatWriter(Stream str, PersistenceHelper persistenceContext, int compatVersion)
		{
			this = new IntermediateFormatWriter(str, 0L, null, persistenceContext, compatVersion, false);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005C2E File Offset: 0x00003E2E
		internal IntermediateFormatWriter(Stream str, List<Declaration> declarations, int compatVersion)
		{
			this = new IntermediateFormatWriter(str, 0L, declarations, null, compatVersion, false);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005C3D File Offset: 0x00003E3D
		internal IntermediateFormatWriter(Stream str, List<Declaration> declarations, int compatVersion, bool prohibitSerializableValues)
		{
			this = new IntermediateFormatWriter(str, 0L, declarations, null, compatVersion, prohibitSerializableValues);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005C4D File Offset: 0x00003E4D
		internal IntermediateFormatWriter(Stream str, List<Declaration> declarations, PersistenceHelper persistenceContext, int compatVersion)
		{
			this = new IntermediateFormatWriter(str, 0L, declarations, persistenceContext, compatVersion, false);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005C5D File Offset: 0x00003E5D
		internal IntermediateFormatWriter(Stream str, List<Declaration> declarations, PersistenceHelper persistenceContext, int compatVersion, bool prohibitSerializableValues)
		{
			this = new IntermediateFormatWriter(str, 0L, declarations, persistenceContext, compatVersion, prohibitSerializableValues);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005C70 File Offset: 0x00003E70
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00005D6A File Offset: 0x00003F6A
		private bool UsesCompatVersion
		{
			get
			{
				return this.m_compatVersion != 0;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00005D75 File Offset: 0x00003F75
		internal MemberInfo CurrentMember
		{
			get
			{
				return this.m_currentMember;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00005D7D File Offset: 0x00003F7D
		internal PersistenceHelper PersistenceHelper
		{
			get
			{
				return this.m_persistenceContext;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00005D85 File Offset: 0x00003F85
		internal bool ProhibitSerializableValues
		{
			get
			{
				return this.m_prohibitSerializableValues;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005D90 File Offset: 0x00003F90
		private void WriteDeclarations(List<Declaration> declarations)
		{
			this.m_writer.WriteListStart(ObjectType.Declaration, declarations.Count);
			for (int i = 0; i < declarations.Count; i++)
			{
				Declaration declaration = declarations[i];
				this.WriteDeclaration(declaration);
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005DD1 File Offset: 0x00003FD1
		private Declaration WriteDeclaration(Declaration decl)
		{
			decl = this.FilterAndStoreDeclaration(decl);
			this.m_writer.Write(decl);
			return decl;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005DEC File Offset: 0x00003FEC
		private void FilterAndStoreDeclarations(List<Declaration> declarations)
		{
			for (int i = 0; i < declarations.Count; i++)
			{
				Declaration declaration = declarations[i];
				this.FilterAndStoreDeclaration(declaration);
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005E1A File Offset: 0x0000401A
		private Declaration FilterAndStoreDeclaration(Declaration decl)
		{
			decl = decl.CreateFilteredDeclarationForWriteVersion(this.m_compatVersion);
			this.m_writtenDecls.Add(decl.ObjectType, decl);
			return decl;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005E40 File Offset: 0x00004040
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

		// Token: 0x0600012F RID: 303 RVA: 0x00005E90 File Offset: 0x00004090
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

		// Token: 0x06000130 RID: 304 RVA: 0x00005ECD File Offset: 0x000040CD
		internal void Write(IPersistable persistableObj)
		{
			this.Write(persistableObj, true);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005ED7 File Offset: 0x000040D7
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

		// Token: 0x06000132 RID: 306 RVA: 0x00005F08 File Offset: 0x00004108
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

		// Token: 0x06000133 RID: 307 RVA: 0x00005F70 File Offset: 0x00004170
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

		// Token: 0x06000134 RID: 308 RVA: 0x00006008 File Offset: 0x00004208
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

		// Token: 0x06000135 RID: 309 RVA: 0x0000609C File Offset: 0x0000429C
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

		// Token: 0x06000136 RID: 310 RVA: 0x00006134 File Offset: 0x00004334
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

		// Token: 0x06000137 RID: 311 RVA: 0x000061DC File Offset: 0x000043DC
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

		// Token: 0x06000138 RID: 312 RVA: 0x00006280 File Offset: 0x00004480
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

		// Token: 0x06000139 RID: 313 RVA: 0x00006324 File Offset: 0x00004524
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

		// Token: 0x0600013A RID: 314 RVA: 0x000063C0 File Offset: 0x000045C0
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

		// Token: 0x0600013B RID: 315 RVA: 0x00006464 File Offset: 0x00004664
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

		// Token: 0x0600013C RID: 316 RVA: 0x0000650C File Offset: 0x0000470C
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

		// Token: 0x0600013D RID: 317 RVA: 0x000065B4 File Offset: 0x000047B4
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

		// Token: 0x0600013E RID: 318 RVA: 0x0000664C File Offset: 0x0000484C
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

		// Token: 0x0600013F RID: 319 RVA: 0x00006718 File Offset: 0x00004918
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

		// Token: 0x06000140 RID: 320 RVA: 0x000067BC File Offset: 0x000049BC
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

		// Token: 0x06000141 RID: 321 RVA: 0x00006850 File Offset: 0x00004A50
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

		// Token: 0x06000142 RID: 322 RVA: 0x000068DC File Offset: 0x00004ADC
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

		// Token: 0x06000143 RID: 323 RVA: 0x00006968 File Offset: 0x00004B68
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

		// Token: 0x06000144 RID: 324 RVA: 0x00006A00 File Offset: 0x00004C00
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

		// Token: 0x06000145 RID: 325 RVA: 0x00006AA8 File Offset: 0x00004CA8
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

		// Token: 0x06000146 RID: 326 RVA: 0x00006B44 File Offset: 0x00004D44
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

		// Token: 0x06000147 RID: 327 RVA: 0x00006BDC File Offset: 0x00004DDC
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

		// Token: 0x06000148 RID: 328 RVA: 0x00006C74 File Offset: 0x00004E74
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

		// Token: 0x06000149 RID: 329 RVA: 0x00006D08 File Offset: 0x00004F08
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

		// Token: 0x0600014A RID: 330 RVA: 0x00006D64 File Offset: 0x00004F64
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

		// Token: 0x0600014B RID: 331 RVA: 0x00006DBF File Offset: 0x00004FBF
		internal void Write<T>(List<T> rifList) where T : IPersistable
		{
			this.WriteRIFList<T>(rifList);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006DC8 File Offset: 0x00004FC8
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

		// Token: 0x0600014D RID: 333 RVA: 0x00006E24 File Offset: 0x00005024
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

		// Token: 0x0600014E RID: 334 RVA: 0x00006E80 File Offset: 0x00005080
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

		// Token: 0x0600014F RID: 335 RVA: 0x00006ED8 File Offset: 0x000050D8
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

		// Token: 0x06000150 RID: 336 RVA: 0x00006F30 File Offset: 0x00005130
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

		// Token: 0x06000151 RID: 337 RVA: 0x00006F80 File Offset: 0x00005180
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

		// Token: 0x06000152 RID: 338 RVA: 0x00006FCF File Offset: 0x000051CF
		internal void WriteListOfPrimitives<T>(List<T> list)
		{
			this.WriteListOfPrimitives<T>(list, true);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006FDC File Offset: 0x000051DC
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

		// Token: 0x06000154 RID: 340 RVA: 0x00007030 File Offset: 0x00005230
		internal void WriteArrayOfListsOfPrimitives<T>(List<T>[] arrayOfLists)
		{
			this.WriteArrayOfListsOfPrimitives<T>(arrayOfLists, true);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000703C File Offset: 0x0000523C
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

		// Token: 0x06000156 RID: 342 RVA: 0x00007080 File Offset: 0x00005280
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

		// Token: 0x06000157 RID: 343 RVA: 0x000070D0 File Offset: 0x000052D0
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

		// Token: 0x06000158 RID: 344 RVA: 0x00007114 File Offset: 0x00005314
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

		// Token: 0x06000159 RID: 345 RVA: 0x00007158 File Offset: 0x00005358
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

		// Token: 0x0600015A RID: 346 RVA: 0x0000719C File Offset: 0x0000539C
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

		// Token: 0x0600015B RID: 347 RVA: 0x000071E0 File Offset: 0x000053E0
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

		// Token: 0x0600015C RID: 348 RVA: 0x00007224 File Offset: 0x00005424
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

		// Token: 0x0600015D RID: 349 RVA: 0x00007268 File Offset: 0x00005468
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

		// Token: 0x0600015E RID: 350 RVA: 0x000072CC File Offset: 0x000054CC
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

		// Token: 0x0600015F RID: 351 RVA: 0x0000732D File Offset: 0x0000552D
		internal void Write(float[] array)
		{
			this.m_writer.Write(array);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000733B File Offset: 0x0000553B
		internal void Write(int[] array)
		{
			this.m_writer.Write(array);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00007349 File Offset: 0x00005549
		internal void Write(long[] array)
		{
			this.m_writer.Write(array);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00007357 File Offset: 0x00005557
		internal void Write(char[] array)
		{
			this.m_writer.Write(array);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00007365 File Offset: 0x00005565
		internal void Write(byte[] array)
		{
			this.m_writer.Write(array);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00007373 File Offset: 0x00005573
		internal void Write(bool[] array)
		{
			this.m_writer.Write(array);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00007381 File Offset: 0x00005581
		internal void Write(double[] array)
		{
			this.m_writer.Write(array);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000738F File Offset: 0x0000558F
		internal void Write(DateTime dateTime)
		{
			this.m_writer.Write(dateTime, this.m_currentMember.Token);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000073A8 File Offset: 0x000055A8
		internal void Write(DateTimeOffset dateTimeOffset)
		{
			this.m_writer.Write(dateTimeOffset);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000073B6 File Offset: 0x000055B6
		internal void Write(TimeSpan timeSpan)
		{
			this.m_writer.Write(timeSpan);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000073C4 File Offset: 0x000055C4
		internal void Write(Guid guid)
		{
			this.m_writer.Write(guid);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000073D2 File Offset: 0x000055D2
		internal void Write(string value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000073E0 File Offset: 0x000055E0
		internal void Write(bool value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000073EE File Offset: 0x000055EE
		internal void Write(short value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000073FC File Offset: 0x000055FC
		internal void Write(int value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000740A File Offset: 0x0000560A
		internal void Write(long value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007418 File Offset: 0x00005618
		internal void Write(ushort value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00007426 File Offset: 0x00005626
		internal void Write(uint value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00007434 File Offset: 0x00005634
		internal void Write(ulong value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00007442 File Offset: 0x00005642
		internal void Write(char value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00007450 File Offset: 0x00005650
		internal void Write(byte value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000745E File Offset: 0x0000565E
		internal void Write(sbyte value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000746C File Offset: 0x0000566C
		internal void Write(float value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000747A File Offset: 0x0000567A
		internal void Write(double value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007488 File Offset: 0x00005688
		internal void Write(decimal value)
		{
			this.m_writer.Write(value);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007496 File Offset: 0x00005696
		internal void Write7BitEncodedInt(int value)
		{
			this.m_writer.WriteEnum(value);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000074A4 File Offset: 0x000056A4
		internal void WriteEnum(int value)
		{
			this.m_writer.WriteEnum(value);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000074B2 File Offset: 0x000056B2
		internal void WriteNull()
		{
			this.m_writer.WriteNull();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000074BF File Offset: 0x000056BF
		internal void Write(CultureInfo threadCulture)
		{
			if (threadCulture != null)
			{
				this.m_writer.Write(threadCulture.LCID);
				return;
			}
			this.m_writer.Write(-1);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000074E2 File Offset: 0x000056E2
		private void WriteReferenceInList(IReferenceable referenceableItem)
		{
			this.WriteReferenceID((referenceableItem != null) ? referenceableItem.ID : (-2));
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000074F7 File Offset: 0x000056F7
		internal void WriteReference(IReferenceable referenceableItem)
		{
			this.WriteReferenceID((referenceableItem != null) ? referenceableItem.ID : (-1));
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000750B File Offset: 0x0000570B
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

		// Token: 0x0600017F RID: 383 RVA: 0x0000753A File Offset: 0x0000573A
		private void WriteGlobalReferenceInList(IGloballyReferenceable globalReference)
		{
			this.WriteGlobalReferenceID((globalReference != null) ? globalReference.GlobalID : (-2));
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000754F File Offset: 0x0000574F
		internal void WriteGlobalReference(IGloballyReferenceable globalReference)
		{
			this.WriteGlobalReferenceID((globalReference != null) ? globalReference.GlobalID : (-1));
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007563 File Offset: 0x00005763
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

		// Token: 0x06000182 RID: 386 RVA: 0x00007592 File Offset: 0x00005792
		internal void Write(ObjectType type)
		{
			this.m_writer.Write(type);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000075A0 File Offset: 0x000057A0
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

		// Token: 0x06000184 RID: 388 RVA: 0x00007651 File Offset: 0x00005851
		internal void WriteSerializable(object obj)
		{
			if (!this.TryWriteSerializable(obj))
			{
				this.m_writer.Write(Token.Null);
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007668 File Offset: 0x00005868
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

		// Token: 0x06000186 RID: 390 RVA: 0x0000774C File Offset: 0x0000594C
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

		// Token: 0x06000187 RID: 391 RVA: 0x0000777F File Offset: 0x0000597F
		internal void Write(object obj)
		{
			this.Write(obj, true, true);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000778B File Offset: 0x0000598B
		internal bool TryWrite(object obj)
		{
			return this.Write(obj, true, false);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007798 File Offset: 0x00005998
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

		// Token: 0x040000E8 RID: 232
		private int m_currentMemberIndex;

		// Token: 0x040000E9 RID: 233
		private Declaration m_currentDeclaration;

		// Token: 0x040000EA RID: 234
		private Dictionary<ObjectType, Declaration> m_writtenDecls;

		// Token: 0x040000EB RID: 235
		private PersistenceBinaryWriter m_writer;

		// Token: 0x040000EC RID: 236
		private PersistenceHelper m_persistenceContext;

		// Token: 0x040000ED RID: 237
		private bool m_isSeekable;

		// Token: 0x040000EE RID: 238
		private int m_lastMemberInfoIndex;

		// Token: 0x040000EF RID: 239
		private MemberInfo m_currentMember;

		// Token: 0x040000F0 RID: 240
		private readonly bool m_prohibitSerializableValues;

		// Token: 0x040000F1 RID: 241
		private int m_compatVersion;

		// Token: 0x040000F2 RID: 242
		private BinaryFormatter m_binaryFormatter;
	}
}
