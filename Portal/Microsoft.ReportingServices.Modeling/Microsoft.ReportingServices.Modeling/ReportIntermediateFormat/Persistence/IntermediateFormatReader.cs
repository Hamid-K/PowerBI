using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000019 RID: 25
	public struct IntermediateFormatReader
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00003825 File Offset: 0x00001A25
		internal IntermediateFormatReader(Stream str, IRIFObjectCreator rifObjectCreator)
		{
			this = new IntermediateFormatReader(str, rifObjectCreator, null, null);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003831 File Offset: 0x00001A31
		internal IntermediateFormatReader(Stream str, IRIFObjectCreator rifObjectCreator, PersistenceHelper persistenceHelper)
		{
			this = new IntermediateFormatReader(str, rifObjectCreator, null, persistenceHelper);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000383D File Offset: 0x00001A3D
		internal IntermediateFormatReader(Stream str, IRIFObjectCreator rifObjectCreator, GlobalIDOwnerCollection globalIDOwnersFromOtherStream)
		{
			this = new IntermediateFormatReader(str, rifObjectCreator, globalIDOwnersFromOtherStream, null);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000384C File Offset: 0x00001A4C
		internal IntermediateFormatReader(Stream str, IRIFObjectCreator rifObjectCreator, GlobalIDOwnerCollection globalIDOwnersFromOtherStream, PersistenceHelper persistenceHelper)
		{
			this = new IntermediateFormatReader(str, rifObjectCreator, globalIDOwnersFromOtherStream, persistenceHelper, null, null, PersistenceFlags.None, true, PersistenceConstants.CurrentCompatVersion);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003870 File Offset: 0x00001A70
		internal IntermediateFormatReader(Stream str, IRIFObjectCreator rifObjectCreator, GlobalIDOwnerCollection globalIDOwnersFromOtherStream, PersistenceHelper persistenceHelper, int compatVersion)
		{
			this = new IntermediateFormatReader(str, rifObjectCreator, globalIDOwnersFromOtherStream, persistenceHelper, null, null, PersistenceFlags.None, true, compatVersion);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003890 File Offset: 0x00001A90
		internal IntermediateFormatReader(Stream str, IRIFObjectCreator rifObjectCreator, GlobalIDOwnerCollection globalIDOwnersFromOtherStream, PersistenceHelper persistenceHelper, List<Declaration> declarations, IntermediateFormatVersion version, PersistenceFlags flags)
		{
			this = new IntermediateFormatReader(str, rifObjectCreator, globalIDOwnersFromOtherStream, persistenceHelper, declarations, version, flags, false, PersistenceConstants.CurrentCompatVersion);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000038B4 File Offset: 0x00001AB4
		private IntermediateFormatReader(Stream str, IRIFObjectCreator rifObjectCreator, GlobalIDOwnerCollection globalIDOwnersFromOtherStream, PersistenceHelper persistenceHelper, List<Declaration> declarations, IntermediateFormatVersion version, PersistenceFlags flags, bool initFromStream, int compatVersion)
		{
			this.m_currentMemberIndex = -1;
			this.m_readDecls = new Dictionary<ObjectType, Declaration>(EqualityComparers.ObjectTypeComparerInstance);
			this.m_currentPersistedDeclaration = null;
			this.m_reader = new PersistenceBinaryReader(str);
			this.m_referenceableItems = new Dictionary<int, IReferenceable>(EqualityComparers.Int32ComparerInstance);
			this.m_memberReferencesCollection = new Dictionary<IPersistable, Dictionary<ObjectType, List<MemberReference>>>();
			this.m_rifObjectCreator = rifObjectCreator;
			this.m_persistenceHelper = persistenceHelper;
			this.m_version = null;
			this.m_persistenceFlags = PersistenceFlags.None;
			this.m_objectStartPosition = 0L;
			this.m_globalIDOwners = null;
			this.m_currentMemberInfoCount = 0;
			this.m_currentMember = null;
			this.m_binaryFormatter = null;
			this.m_compatVersion = compatVersion;
			if (globalIDOwnersFromOtherStream == null)
			{
				this.m_globalIDOwners = new GlobalIDOwnerCollection();
			}
			else
			{
				this.m_globalIDOwners = globalIDOwnersFromOtherStream;
			}
			if (initFromStream)
			{
				this.m_version = this.ReadIntermediateFormatVersion();
				this.m_persistenceFlags = (PersistenceFlags)this.m_reader.ReadEnum();
				if (IntermediateFormatReader.HasPersistenceFlag(this.m_persistenceFlags, PersistenceFlags.CompatVersioned))
				{
					IncompatibleRIFVersionException.ThrowIfIncompatible(this.m_reader.ReadInt32(), this.m_compatVersion);
				}
				if (IntermediateFormatReader.HasPersistenceFlag(this.m_persistenceFlags, PersistenceFlags.Seekable))
				{
					this.ReadDeclarations();
				}
			}
			else
			{
				this.m_version = version;
				this.m_persistenceFlags = flags;
				this.PrepareDeclarationsFromInitialization(declarations);
			}
			this.m_objectStartPosition = this.m_reader.StreamPosition;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000039EA File Offset: 0x00001BEA
		private static bool HasPersistenceFlag(PersistenceFlags flags, PersistenceFlags flagToTest)
		{
			return (flags & flagToTest) > PersistenceFlags.None;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000039F2 File Offset: 0x00001BF2
		internal bool CanSeek
		{
			get
			{
				return IntermediateFormatReader.HasPersistenceFlag(this.m_persistenceFlags, PersistenceFlags.Seekable);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003A00 File Offset: 0x00001C00
		internal bool EOS
		{
			get
			{
				return this.m_reader.EOS;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003A0D File Offset: 0x00001C0D
		internal IntermediateFormatVersion IntermediateFormatVersion
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003A15 File Offset: 0x00001C15
		internal MemberInfo CurrentMember
		{
			get
			{
				return this.m_currentMember;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003A1D File Offset: 0x00001C1D
		internal PersistenceHelper PersistenceHelper
		{
			get
			{
				return this.m_persistenceHelper;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003A25 File Offset: 0x00001C25
		internal long ObjectStartPosition
		{
			get
			{
				return this.m_objectStartPosition;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003A2D File Offset: 0x00001C2D
		internal bool HasReferences
		{
			get
			{
				return this.m_memberReferencesCollection != null && this.m_memberReferencesCollection.Count > 0;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003A47 File Offset: 0x00001C47
		internal GlobalIDOwnerCollection GlobalIDOwners
		{
			get
			{
				return this.m_globalIDOwners;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003A50 File Offset: 0x00001C50
		private void PrepareDeclarationsFromInitialization(List<Declaration> declarations)
		{
			for (int i = 0; i < declarations.Count; i++)
			{
				Declaration declaration = declarations[i].CreateFilteredDeclarationForWriteVersion(this.m_compatVersion);
				this.m_readDecls.Add(declaration.ObjectType, declaration);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003A94 File Offset: 0x00001C94
		private void ReadDeclarations()
		{
			int num;
			if (this.m_reader.ReadListStart(ObjectType.Declaration, out num))
			{
				for (int i = 0; i < num; i++)
				{
					Declaration declaration = this.m_reader.ReadDeclaration();
					this.m_readDecls.Add(declaration.ObjectType, declaration);
				}
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003ADC File Offset: 0x00001CDC
		internal void RegisterDeclaration(Declaration declaration)
		{
			this.m_currentMemberIndex = -1;
			if (!this.m_readDecls.TryGetValue(declaration.ObjectType, out this.m_currentPersistedDeclaration))
			{
				this.m_currentPersistedDeclaration = this.m_reader.ReadDeclaration();
				this.m_currentPersistedDeclaration.RegisterCurrentDeclaration(declaration);
				this.m_readDecls.Add(declaration.ObjectType, this.m_currentPersistedDeclaration);
			}
			else if (!this.m_currentPersistedDeclaration.RegisteredCurrentDeclaration)
			{
				this.m_currentPersistedDeclaration.RegisterCurrentDeclaration(declaration);
			}
			this.m_currentMemberInfoCount = this.m_currentPersistedDeclaration.MemberInfoList.Count;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003B70 File Offset: 0x00001D70
		internal bool NextMember()
		{
			this.m_currentMemberIndex++;
			if (this.m_currentMemberIndex >= this.m_currentMemberInfoCount)
			{
				return false;
			}
			if (this.m_currentPersistedDeclaration.HasSkippedMembers && this.m_currentPersistedDeclaration.IsMemberSkipped(this.m_currentMemberIndex))
			{
				this.SkipMembers(this.m_currentPersistedDeclaration.MembersToSkip(this.m_currentMemberIndex));
				return this.NextMember();
			}
			this.m_currentMember = this.m_currentPersistedDeclaration.MemberInfoList[this.m_currentMemberIndex];
			return true;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003BF8 File Offset: 0x00001DF8
		internal void ResolveReferences()
		{
			foreach (KeyValuePair<IPersistable, Dictionary<ObjectType, List<MemberReference>>> keyValuePair in this.m_memberReferencesCollection)
			{
				keyValuePair.Key.ResolveReferences(keyValuePair.Value, this.m_referenceableItems);
				keyValuePair.Value.Clear();
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003C6C File Offset: 0x00001E6C
		internal void ClearReferences()
		{
			this.m_referenceableItems = new Dictionary<int, IReferenceable>(EqualityComparers.Int32ComparerInstance);
			this.m_memberReferencesCollection = new Dictionary<IPersistable, Dictionary<ObjectType, List<MemberReference>>>();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003C8C File Offset: 0x00001E8C
		private void SkipMembers(int toSkip)
		{
			int i = 0;
			while (i < toSkip)
			{
				this.m_currentMember = this.m_currentPersistedDeclaration.MemberInfoList[this.m_currentMemberIndex];
				switch (this.m_currentMember.ObjectType)
				{
				case ObjectType.Null:
					break;
				case ObjectType.None:
					this.SkipPrimitive(this.m_currentMember.Token);
					break;
				case ObjectType.RIFObjectArray:
					if (this.m_currentMember.Token == Token.Reference)
					{
						this.SkipListOrArrayOfReferences();
					}
					else
					{
						this.SkipListOrArrayOfRIFObjects();
					}
					break;
				case ObjectType.RIFObjectList:
					if (this.m_currentMember.Token == Token.Reference)
					{
						this.SkipListOrArrayOfReferences();
					}
					else
					{
						this.SkipListOrArrayOfRIFObjects();
					}
					break;
				case ObjectType.PrimitiveArray:
					this.SkipArrayOfPrimitives();
					break;
				case ObjectType.PrimitiveList:
					this.SkipListOfPrimitives();
					break;
				case ObjectType.PrimitiveTypedArray:
					switch (this.m_currentMember.Token)
					{
					case Token.DateTimeOffset:
					case Token.Guid:
						this.m_reader.SkipBytes(16);
						goto IL_01FD;
					case Token.DateTime:
					case Token.TimeSpan:
					case Token.Int64:
					case Token.UInt64:
					case Token.Double:
						this.m_reader.SkipTypedArray(8);
						goto IL_01FD;
					case Token.Char:
					case Token.Int16:
					case Token.UInt16:
						this.m_reader.SkipTypedArray(2);
						goto IL_01FD;
					case Token.Int32:
					case Token.UInt32:
					case Token.Single:
						this.m_reader.SkipTypedArray(4);
						goto IL_01FD;
					case Token.Byte:
					case Token.SByte:
						this.m_reader.SkipTypedArray(1);
						goto IL_01FD;
					case Token.Decimal:
						this.m_reader.ReadDecimal();
						goto IL_01FD;
					}
					Global.Tracer.Assert(false);
					break;
				case ObjectType.StringRIFObjectDictionary:
					this.SkipStringRIFObjectDictionary();
					break;
				case ObjectType.StringRIFObjectHashtable:
				case ObjectType.NameObjectCollection:
					goto IL_01DC;
				case ObjectType.Int32RIFObjectDictionary:
					this.SkipInt32RIFObjectDictionary();
					break;
				case ObjectType.Int32PrimitiveListHashtable:
					this.SkipInt32PrimitiveListHashtable();
					break;
				case ObjectType.ObjectHashtableHashtable:
					this.SkipObjectHashtableHashtable();
					break;
				case ObjectType.StringObjectHashtable:
					this.SkipStringObjectHashtable();
					break;
				default:
					goto IL_01DC;
				}
				IL_01FD:
				this.m_currentMemberIndex++;
				i++;
				continue;
				IL_01DC:
				if (this.m_currentMember.Token == Token.Reference)
				{
					this.m_reader.SkipMultiByteInt();
					goto IL_01FD;
				}
				this.SkipRIFObject();
				goto IL_01FD;
			}
			this.m_currentMemberIndex--;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003EC0 File Offset: 0x000020C0
		private void SkipPrimitive(Token token)
		{
			switch (token)
			{
			case Token.Null:
				break;
			case Token.Object:
				this.SkipPrimitive(this.m_reader.ReadToken());
				return;
			case Token.Reference:
			case Token.Enum:
				this.m_reader.SkipMultiByteInt();
				return;
			default:
				switch (token)
				{
				case Token.DateTimeOffset:
				case Token.Guid:
				case Token.Decimal:
					this.m_reader.SkipBytes(16);
					return;
				case Token.String:
					this.m_reader.SkipString();
					return;
				case Token.DateTime:
				case Token.TimeSpan:
				case Token.Int64:
				case Token.UInt64:
				case Token.Double:
					this.m_reader.SkipBytes(8);
					return;
				case Token.Char:
				case Token.Int16:
				case Token.UInt16:
					this.m_reader.SkipBytes(2);
					return;
				case Token.Boolean:
				case Token.Byte:
				case Token.SByte:
					this.m_reader.SkipBytes(1);
					return;
				case Token.Int32:
				case Token.UInt32:
				case Token.Single:
					this.m_reader.SkipBytes(4);
					return;
				}
				Global.Tracer.Assert(false);
				break;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003FB4 File Offset: 0x000021B4
		private void SkipArrayOfPrimitives()
		{
			int num;
			if (this.m_reader.ReadArrayStart(this.m_currentMember.ObjectType, out num))
			{
				for (int i = 0; i < num; i++)
				{
					this.SkipPrimitive(this.m_reader.ReadToken());
				}
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003FF8 File Offset: 0x000021F8
		private void SkipListOfPrimitives()
		{
			int num;
			if (this.m_reader.ReadListStart(this.m_currentMember.ObjectType, out num))
			{
				for (int i = 0; i < num; i++)
				{
					this.SkipPrimitive(this.m_reader.ReadToken());
				}
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000403C File Offset: 0x0000223C
		private void SkipListOrArrayOfReferences()
		{
			int num;
			if (this.m_reader.ReadListStart(this.m_currentMember.ObjectType, out num))
			{
				for (int i = 0; i < num; i++)
				{
					if (this.m_reader.ReadObjectType() != ObjectType.Null)
					{
						this.m_reader.SkipMultiByteInt();
					}
				}
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004088 File Offset: 0x00002288
		private void SkipListOrArrayOfRIFObjects()
		{
			int num;
			if (this.m_reader.ReadListStart(this.m_currentMember.ObjectType, out num))
			{
				for (int i = 0; i < num; i++)
				{
					this.SkipRIFObject();
				}
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000040C4 File Offset: 0x000022C4
		private void SkipRIFObject()
		{
			ObjectType objectType = this.m_reader.ReadObjectType();
			if (objectType != ObjectType.Null)
			{
				((IntermediateFormatReader)base.MemberwiseClone()).__SkipRIFObjectPrivate(objectType);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004100 File Offset: 0x00002300
		private void __SkipRIFObjectPrivate(ObjectType objectType)
		{
			this.m_currentMemberIndex = 0;
			if (!this.m_readDecls.TryGetValue(objectType, out this.m_currentPersistedDeclaration))
			{
				this.m_currentPersistedDeclaration = this.m_reader.ReadDeclaration();
				this.m_readDecls.Add(objectType, this.m_currentPersistedDeclaration);
			}
			this.m_currentMemberInfoCount = this.m_currentPersistedDeclaration.MemberInfoList.Count;
			this.SkipMembers(this.m_currentMemberInfoCount);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004170 File Offset: 0x00002370
		private void SkipStringRIFObjectDictionary()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				for (int i = 0; i < num; i++)
				{
					this.m_reader.SkipString();
					this.SkipRIFObject();
				}
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000041B4 File Offset: 0x000023B4
		private void SkipInt32RIFObjectDictionary()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				for (int i = 0; i < num; i++)
				{
					this.m_reader.SkipBytes(4);
					this.SkipRIFObject();
				}
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000041FC File Offset: 0x000023FC
		internal void SkipInt32PrimitiveListHashtable()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				for (int i = 0; i < num; i++)
				{
					this.m_reader.SkipBytes(4);
					this.SkipListOfPrimitives();
				}
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004244 File Offset: 0x00002444
		internal void SkipStringObjectHashtable()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				for (int i = 0; i < num; i++)
				{
					this.m_reader.SkipString();
					this.SkipPrimitive(this.m_reader.ReadToken());
				}
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004294 File Offset: 0x00002494
		internal void SkipObjectHashtableHashtable()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				for (int i = 0; i < num; i++)
				{
					this.SkipPrimitive(this.m_reader.ReadToken());
					this.SkipObjectHashtableHashtable();
				}
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000042DE File Offset: 0x000024DE
		internal void Seek(long newPosition)
		{
			this.Seek(newPosition, SeekOrigin.Begin);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000042E8 File Offset: 0x000024E8
		internal void Seek(long newPosition, SeekOrigin seekOrigin)
		{
			this.m_reader.Seek(newPosition, seekOrigin);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000042F7 File Offset: 0x000024F7
		internal IPersistable ReadRIFObject()
		{
			return this.ReadRIFObject(true);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004300 File Offset: 0x00002500
		internal IPersistable ReadRIFObject(IPersistable persitObj)
		{
			ObjectType objectType = this.ReadRIFObjectStart();
			if (objectType != ObjectType.Null)
			{
				persitObj.Deserialize(this);
				this.ReadRIFObjectFinish(objectType, persitObj, false);
			}
			else
			{
				persitObj = null;
			}
			return persitObj;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004334 File Offset: 0x00002534
		internal T ReadRIFObject<T>() where T : IPersistable, new()
		{
			ObjectType objectType = this.ReadRIFObjectStart();
			T t = default(T);
			if (objectType != ObjectType.Null)
			{
				t = new T();
				t.Deserialize(this);
				this.ReadRIFObjectFinish(objectType, t, false);
			}
			return t;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000437C File Offset: 0x0000257C
		private IPersistable ReadRIFObject(bool verify)
		{
			ObjectType objectType = this.ReadRIFObjectStart();
			IPersistable persistable = null;
			if (objectType != ObjectType.Null)
			{
				persistable = this.m_rifObjectCreator.CreateRIFObject(objectType, ref this);
				this.AddReferenceableItem(persistable);
			}
			return persistable;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000043AC File Offset: 0x000025AC
		private void AddReferenceableItem(IPersistable persistObj)
		{
			IReferenceable referenceable = persistObj as IReferenceable;
			if (referenceable != null && referenceable.ID != -2)
			{
				this.m_referenceableItems.Add(referenceable.ID, referenceable);
			}
			IGlobalIDOwner globalIDOwner = persistObj as IGlobalIDOwner;
			if (globalIDOwner != null)
			{
				int globalID = this.m_globalIDOwners.GetGlobalID();
				globalIDOwner.GlobalID = globalID;
				IGloballyReferenceable globallyReferenceable = persistObj as IGloballyReferenceable;
				if (globallyReferenceable != null)
				{
					this.m_globalIDOwners.Add(globallyReferenceable);
				}
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004412 File Offset: 0x00002612
		private ObjectType ReadRIFObjectStart()
		{
			this.m_objectStartPosition = this.m_reader.StreamPosition;
			return this.m_reader.ReadObjectType();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004430 File Offset: 0x00002630
		private void ReadRIFObjectFinish(ObjectType persistedType, IPersistable persitObj, bool verify)
		{
			bool flag = this.m_currentPersistedDeclaration != null && verify;
			if (persitObj is IReferenceable)
			{
				IReferenceable referenceable = (IReferenceable)persitObj;
				this.m_referenceableItems.Add(referenceable.ID, referenceable);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000446C File Offset: 0x0000266C
		internal Dictionary<string, TValue> ReadStringRIFObjectDictionary<TValue>() where TValue : IPersistable
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				Dictionary<string, TValue> dictionary = new Dictionary<string, TValue>(num, EqualityComparers.StringComparerInstance);
				for (int i = 0; i < num; i++)
				{
					dictionary.Add(this.ReadString(false), (TValue)((object)this.ReadRIFObject()));
				}
				return dictionary;
			}
			return null;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000044C8 File Offset: 0x000026C8
		internal Dictionary<int, TValue> ReadInt32RIFObjectDictionary<TValue>() where TValue : IPersistable
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				Dictionary<int, TValue> dictionary = new Dictionary<int, TValue>(num, EqualityComparers.Int32ComparerInstance);
				for (int i = 0; i < num; i++)
				{
					dictionary.Add(this.ReadInt32(false), (TValue)((object)this.ReadRIFObject()));
				}
				return dictionary;
			}
			return null;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004524 File Offset: 0x00002724
		internal IDictionary ReadInt32RIFObjectDictionary<T>(CreateDictionary<T> dictionaryCreator) where T : IDictionary
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				IDictionary dictionary = dictionaryCreator(num);
				for (int i = 0; i < num; i++)
				{
					dictionary.Add(this.ReadInt32(false), this.ReadRIFObject());
				}
				return dictionary;
			}
			return null;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004580 File Offset: 0x00002780
		internal T ReadInt32PrimitiveListHashtable<T, U>() where T : Hashtable, new() where U : struct
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				T t = new T();
				for (int i = 0; i < num; i++)
				{
					t.Add(this.ReadInt32(false), this.ReadListOfPrimitives<U>());
				}
				return t;
			}
			return default(T);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000045E4 File Offset: 0x000027E4
		internal T ReadStringInt32Hashtable<T>() where T : IDictionary, new()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				T t = new T();
				for (int i = 0; i < num; i++)
				{
					ref T ptr = ref t;
					if (default(T) == null)
					{
						T t2 = t;
						ptr = ref t2;
					}
					ptr.Add(this.ReadString(false), this.ReadInt32());
				}
				return t;
			}
			return default(T);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004660 File Offset: 0x00002860
		internal T ReadByteVariantHashtable<T>() where T : IDictionary, new()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				T t = new T();
				for (int i = 0; i < num; i++)
				{
					ref T ptr = ref t;
					if (default(T) == null)
					{
						T t2 = t;
						ptr = ref t2;
					}
					ptr.Add(this.ReadByte(false), this.ReadVariant());
				}
				return t;
			}
			return default(T);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000046DC File Offset: 0x000028DC
		internal T ReadStringStringHashtable<T>() where T : IDictionary, new()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				T t = new T();
				for (int i = 0; i < num; i++)
				{
					ref T ptr = ref t;
					if (default(T) == null)
					{
						T t2 = t;
						ptr = ref t2;
					}
					ptr.Add(this.ReadString(false), this.ReadString(false));
				}
				return t;
			}
			return default(T);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004754 File Offset: 0x00002954
		internal T ReadStringObjectHashtable<T>() where T : IDictionary, new()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				T t = new T();
				for (int i = 0; i < num; i++)
				{
					ref T ptr = ref t;
					if (default(T) == null)
					{
						T t2 = t;
						ptr = ref t2;
					}
					ptr.Add(this.ReadString(false), this.ReadVariant());
				}
				return t;
			}
			return default(T);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000047CC File Offset: 0x000029CC
		internal T ReadStringRIFObjectHashtable<T>() where T : IDictionary, new()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				T t = new T();
				for (int i = 0; i < num; i++)
				{
					ref T ptr = ref t;
					if (default(T) == null)
					{
						T t2 = t;
						ptr = ref t2;
					}
					ptr.Add(this.ReadString(false), this.ReadRIFObject());
				}
				return t;
			}
			return default(T);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004844 File Offset: 0x00002A44
		internal Dictionary<string, List<string>> ReadStringListOfStringDictionary()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
				for (int i = 0; i < num; i++)
				{
					dictionary.Add(this.ReadString(false), this.ReadListOfPrimitives<string>());
				}
				return dictionary;
			}
			return null;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004894 File Offset: 0x00002A94
		internal T ReadStringObjectHashtable<T>(CreateDictionary<T> createDictionary, Predicate<string> allowKey, Converter<string, string> processName, Converter<object, object> processValue) where T : IDictionary
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				T t = createDictionary(num);
				for (int i = 0; i < num; i++)
				{
					string text = this.ReadString(false);
					object obj = this.ReadVariant();
					if (allowKey(text))
					{
						ref T ptr = ref t;
						if (default(T) == null)
						{
							T t2 = t;
							ptr = ref t2;
						}
						ptr.Add(processName(text), processValue(obj));
					}
				}
				return t;
			}
			return default(T);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000492C File Offset: 0x00002B2C
		internal Hashtable ReadObjectHashtableHashtable()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				Hashtable hashtable = new Hashtable();
				for (int i = 0; i < num; i++)
				{
					hashtable.Add(this.ReadVariant(), this.ReadVariantVariantHashtable());
				}
				return hashtable;
			}
			return null;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000497C File Offset: 0x00002B7C
		internal Hashtable ReadNLevelVariantHashtable()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				Hashtable hashtable = new Hashtable();
				for (int i = 0; i < num; i++)
				{
					object obj = this.ReadVariant();
					Token token = this.m_reader.ReadToken();
					object obj2;
					if (token != Token.Object)
					{
						if (token == Token.Hashtable)
						{
							obj2 = this.ReadNLevelVariantHashtable();
						}
						else
						{
							Global.Tracer.Assert(false, "Invalid token: {0} while reading NLevelVariantHashtable", new object[] { token });
							obj2 = null;
						}
					}
					else
					{
						obj2 = this.ReadVariant();
					}
					hashtable.Add(obj, obj2);
				}
				return hashtable;
			}
			return null;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004A1C File Offset: 0x00002C1C
		internal T ReadNameObjectCollection<T>() where T : class, INameObjectCollection, new()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				T t = new T();
				for (int i = 0; i < num; i++)
				{
					t.Add(this.ReadString(false), this.ReadVariant());
				}
				return t;
			}
			return default(T);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004A78 File Offset: 0x00002C78
		internal T? ReadNullable<T>() where T : struct
		{
			return (T?)this.ReadVariant();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004A85 File Offset: 0x00002C85
		internal Dictionary<T, string> ReadRIFObjectStringHashtable<T>() where T : IPersistable
		{
			return this.ReadRIFObjectStringHashtable<T>(null);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004A90 File Offset: 0x00002C90
		internal Dictionary<T, string> ReadRIFObjectStringHashtable<T>(Dictionary<T, string> dictionary) where T : IPersistable
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				if (dictionary == null)
				{
					dictionary = new Dictionary<T, string>(num);
				}
				for (int i = 0; i < num; i++)
				{
					dictionary.Add((T)((object)this.ReadRIFObject()), this.ReadString(false));
				}
			}
			return dictionary;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004AE8 File Offset: 0x00002CE8
		internal Hashtable ReadVariantVariantHashtable()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				Hashtable hashtable = new Hashtable();
				for (int i = 0; i < num; i++)
				{
					hashtable.Add(this.ReadVariant(), this.ReadVariant());
				}
				return hashtable;
			}
			return null;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004B38 File Offset: 0x00002D38
		internal Dictionary<List<object>, object> ReadVariantListVariantDictionary()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				Dictionary<List<object>, object> dictionary = new Dictionary<List<object>, object>();
				for (int i = 0; i < num; i++)
				{
					dictionary.Add(this.ReadListOfVariant<List<object>>(), this.ReadVariant());
				}
				return dictionary;
			}
			return null;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004B88 File Offset: 0x00002D88
		internal Dictionary<string, List<object>> ReadStringVariantListDictionary()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				Dictionary<string, List<object>> dictionary = new Dictionary<string, List<object>>();
				for (int i = 0; i < num; i++)
				{
					dictionary.Add(this.ReadString(), this.ReadListOfVariant<List<object>>());
				}
				return dictionary;
			}
			return null;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004BD8 File Offset: 0x00002DD8
		internal Dictionary<string, bool[]> ReadStringBoolArrayDictionary()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				Dictionary<string, bool[]> dictionary = new Dictionary<string, bool[]>();
				for (int i = 0; i < num; i++)
				{
					dictionary.Add(this.ReadString(false), this.m_reader.ReadBooleanArray());
				}
				return dictionary;
			}
			return null;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004C2C File Offset: 0x00002E2C
		internal Hashtable ReadInt32StringHashtable()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				Hashtable hashtable = new Hashtable();
				for (int i = 0; i < num; i++)
				{
					hashtable.Add(this.ReadInt32(false), this.ReadString(false));
				}
				return hashtable;
			}
			return null;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004C84 File Offset: 0x00002E84
		internal T ReadVariantRIFObjectDictionary<T>(CreateDictionary<T> creator) where T : IDictionary, new()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				T t = creator(num);
				for (int i = 0; i < num; i++)
				{
					ref T ptr = ref t;
					if (default(T) == null)
					{
						T t2 = t;
						ptr = ref t2;
					}
					ptr.Add(this.ReadVariant(), this.ReadRIFObject());
				}
				return t;
			}
			return default(T);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004CFC File Offset: 0x00002EFC
		internal T ReadVariantListOfRIFObjectDictionary<T, V>(CreateDictionary<T> creator) where T : IDictionary, new() where V : class, IList, new()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				T t = creator(num);
				for (int i = 0; i < num; i++)
				{
					ref T ptr = ref t;
					if (default(T) == null)
					{
						T t2 = t;
						ptr = ref t2;
					}
					ptr.Add(this.ReadVariant(), this.ReadListOfRIFObjects<V>());
				}
				return t;
			}
			return default(T);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004D78 File Offset: 0x00002F78
		internal Dictionary<int, object> Int32SerializableDictionary()
		{
			int num;
			if (this.m_reader.ReadDictionaryStart(this.m_currentMember.ObjectType, out num))
			{
				Dictionary<int, object> dictionary = new Dictionary<int, object>();
				for (int i = 0; i < num; i++)
				{
					dictionary.Add(this.ReadInt32(false), this.ReadSerializable());
				}
				return dictionary;
			}
			return null;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004DC8 File Offset: 0x00002FC8
		internal T ReadListOfRIFObjects<T>() where T : class, IList, new()
		{
			int num;
			if (this.m_reader.ReadListStart(this.m_currentMember.ObjectType, out num))
			{
				T t = new T();
				for (int i = 0; i < num; i++)
				{
					t.Add(this.ReadRIFObject());
				}
				return t;
			}
			return default(T);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004E20 File Offset: 0x00003020
		internal void ReadListOfRIFObjects(IList list)
		{
			int num;
			if (this.m_reader.ReadListStart(this.m_currentMember.ObjectType, out num))
			{
				for (int i = 0; i < num; i++)
				{
					list.Add(this.ReadRIFObject());
				}
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004E60 File Offset: 0x00003060
		internal void ReadListOfRIFObjects<T>(Action<T> addRIFObject) where T : IPersistable
		{
			int num;
			if (this.m_reader.ReadListStart(this.m_currentMember.ObjectType, out num))
			{
				for (int i = 0; i < num; i++)
				{
					addRIFObject((T)((object)this.ReadRIFObject()));
				}
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004EA4 File Offset: 0x000030A4
		internal List<T> ReadGenericListOfRIFObjects<T>() where T : IPersistable
		{
			int num;
			if (this.m_reader.ReadListStart(this.m_currentMember.ObjectType, out num))
			{
				List<T> list = new List<T>(num);
				for (int i = 0; i < num; i++)
				{
					list.Add((T)((object)this.ReadRIFObject()));
				}
				return list;
			}
			return null;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004EF4 File Offset: 0x000030F4
		internal List<T> ReadGenericListOfRIFObjectsUsingNew<T>() where T : IPersistable, new()
		{
			int num;
			if (this.m_reader.ReadListStart(this.m_currentMember.ObjectType, out num))
			{
				List<T> list = new List<T>(num);
				for (int i = 0; i < num; i++)
				{
					list.Add(this.ReadRIFObject<T>());
				}
				return list;
			}
			return null;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004F40 File Offset: 0x00003140
		internal List<T> ReadGenericListOfRIFObjects<T>(Action<T> action) where T : IPersistable
		{
			int num;
			if (this.m_reader.ReadListStart(this.m_currentMember.ObjectType, out num))
			{
				List<T> list = new List<T>(num);
				for (int i = 0; i < num; i++)
				{
					T t = (T)((object)this.ReadRIFObject());
					action(t);
					list.Add(t);
				}
				return list;
			}
			return null;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004F98 File Offset: 0x00003198
		internal List<List<T>> ReadListOfListsOfRIFObjects<T>() where T : IPersistable
		{
			int num;
			if (this.m_reader.ReadListStart(this.m_currentMember.ObjectType, out num))
			{
				List<List<T>> list = new List<List<T>>(num);
				for (int i = 0; i < num; i++)
				{
					List<T> list2 = this.ReadGenericListOfRIFObjects<T>();
					list.Add(list2);
				}
				return list;
			}
			return null;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004FE4 File Offset: 0x000031E4
		internal List<T[]> ReadListOfRIFObjectArrays<T>() where T : IPersistable
		{
			int num;
			if (this.m_reader.ReadListStart(this.m_currentMember.ObjectType, out num))
			{
				List<T[]> list = new List<T[]>(num);
				for (int i = 0; i < num; i++)
				{
					T[] array = this.ReadArrayOfRIFObjects<T>();
					list.Add(array);
				}
				return list;
			}
			return null;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005030 File Offset: 0x00003230
		internal List<T> ReadListOfPrimitives<T>()
		{
			int num;
			if (this.m_reader.ReadListStart(ObjectType.PrimitiveList, out num))
			{
				List<T> list = new List<T>(num);
				for (int i = 0; i < num; i++)
				{
					list.Add((T)((object)this.ReadVariant()));
				}
				return list;
			}
			return null;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005074 File Offset: 0x00003274
		internal List<List<T>[]> ReadListOfArrayOfListsOfPrimitives<T>()
		{
			int num;
			if (this.m_reader.ReadListStart(ObjectType.PrimitiveList, out num))
			{
				List<List<T>[]> list = new List<List<T>[]>(num);
				for (int i = 0; i < num; i++)
				{
					list.Add(this.ReadArrayOfListsOfPrimitives<T>());
				}
				return list;
			}
			return null;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000050B4 File Offset: 0x000032B4
		internal T ReadListOfVariant<T>() where T : class, IList, new()
		{
			int num;
			if (this.m_reader.ReadListStart(this.m_currentMember.ObjectType, out num))
			{
				T t = new T();
				for (int i = 0; i < num; i++)
				{
					t.Add(this.ReadVariant());
				}
				return t;
			}
			return default(T);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000510C File Offset: 0x0000330C
		internal List<T>[] ReadArrayOfListsOfPrimitives<T>()
		{
			int num;
			if (this.m_reader.ReadArrayStart(ObjectType.PrimitiveArray, out num))
			{
				List<T>[] array = new List<T>[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = this.ReadListOfPrimitives<T>();
				}
				return array;
			}
			return null;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005148 File Offset: 0x00003348
		internal List<T>[] ReadArrayOfRIFObjectLists<T>() where T : IPersistable
		{
			int num;
			if (this.m_reader.ReadArrayStart(ObjectType.RIFObjectArray, out num))
			{
				List<T>[] array = new List<T>[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = this.ReadGenericListOfRIFObjects<T>();
				}
				return array;
			}
			return null;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005184 File Offset: 0x00003384
		internal T[] ReadArrayOfRIFObjects<T>() where T : IPersistable
		{
			return this.ReadArrayOfRIFObjects<T>(true);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005190 File Offset: 0x00003390
		private T[] ReadArrayOfRIFObjects<T>(bool verify) where T : IPersistable
		{
			int num;
			if (this.m_reader.ReadArrayStart(ObjectType.RIFObjectArray, out num))
			{
				T[] array = new T[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = (T)((object)this.ReadRIFObject(verify));
				}
				return array;
			}
			return null;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000051D8 File Offset: 0x000033D8
		internal T[,][] Read2DArrayOfArrayOfRIFObjects<T>() where T : IPersistable
		{
			int num = -1;
			int num2 = -1;
			if (this.m_reader.Read2DArrayStart(ObjectType.Array2D, out num, out num2))
			{
				T[,][] array = new T[num, num2][];
				for (int i = 0; i < num; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						array[i, j] = this.ReadArrayOfRIFObjects<T>(false);
					}
				}
				return array;
			}
			return null;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005234 File Offset: 0x00003434
		internal T[,] Read2DArrayOfRIFObjects<T>() where T : IPersistable
		{
			int num = -1;
			int num2 = -1;
			if (this.m_reader.Read2DArrayStart(ObjectType.Array2D, out num, out num2))
			{
				T[,] array = new T[num, num2];
				for (int i = 0; i < num; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						array[i, j] = (T)((object)this.ReadRIFObject());
					}
				}
				return array;
			}
			return null;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005294 File Offset: 0x00003494
		internal T[] ReadArrayOfRIFObjects<RIFT, T>(Converter<RIFT, T> convertRIFObject) where RIFT : IPersistable
		{
			int num;
			if (this.m_reader.ReadArrayStart(ObjectType.RIFObjectArray, out num))
			{
				T[] array = new T[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = convertRIFObject((RIFT)((object)this.ReadRIFObject()));
				}
				return array;
			}
			return null;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000052E0 File Offset: 0x000034E0
		internal string[] ReadStringArray()
		{
			int num;
			if (this.m_reader.ReadArrayStart(this.m_currentMember.ObjectType, out num))
			{
				string[] array = new string[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = this.ReadString();
				}
				return array;
			}
			return null;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005328 File Offset: 0x00003528
		internal object[] ReadVariantArray()
		{
			int num;
			if (this.m_reader.ReadArrayStart(this.m_currentMember.ObjectType, out num))
			{
				object[] array = new object[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = this.ReadVariant();
				}
				return array;
			}
			return null;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005370 File Offset: 0x00003570
		internal object[] ReadSerializableArray()
		{
			int num;
			if (this.m_reader.ReadArrayStart(this.m_currentMember.ObjectType, out num))
			{
				object[] array = new object[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = this.ReadSerializable();
				}
				return array;
			}
			return null;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000053B8 File Offset: 0x000035B8
		internal object ReadSerializable()
		{
			Token token = this.m_reader.ReadToken();
			if (token == Token.Serializable)
			{
				return this.ReadISerializable();
			}
			return this.ReadVariant(token);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000053E8 File Offset: 0x000035E8
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		private object ReadISerializable()
		{
			object obj;
			try
			{
				if (this.m_binaryFormatter == null)
				{
					this.m_binaryFormatter = new BinaryFormatter();
				}
				obj = this.m_binaryFormatter.Deserialize(this.m_reader.BaseStream);
			}
			catch (Exception ex)
			{
				throw new RSException(ErrorCode.rsProcessingError, ErrorStringsWrapper.Keys.GetString(ErrorCode.rsProcessingError.ToString()), ex, Global.Tracer, null, Array.Empty<object>());
			}
			return obj;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000545C File Offset: 0x0000365C
		internal object ReadVariant()
		{
			Token token = this.m_reader.ReadToken();
			return this.ReadVariant(token);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000547C File Offset: 0x0000367C
		private object ReadVariant(Token token)
		{
			if (token == Token.Null)
			{
				return null;
			}
			if (token == Token.Object)
			{
				return this.ReadRIFObject(false);
			}
			switch (token)
			{
			case Token.SqlGeometry:
				return this.m_reader.ReadSqlGeometry();
			case Token.SqlGeography:
				return this.m_reader.ReadSqlGeography();
			case Token.DateTimeWithKind:
				return this.m_reader.ReadDateTimeWithKind();
			case Token.DateTimeOffset:
				return this.m_reader.ReadDateTimeOffset();
			case Token.ByteArray:
				return this.m_reader.ReadByteArray();
			case Token.Guid:
				return this.m_reader.ReadGuid();
			case Token.String:
				return this.m_reader.ReadString(false);
			case Token.DateTime:
				return this.m_reader.ReadDateTime();
			case Token.TimeSpan:
				return this.m_reader.ReadTimeSpan();
			case Token.Char:
				return this.m_reader.ReadChar();
			case Token.Boolean:
				return this.m_reader.ReadBoolean();
			case Token.Int16:
				return this.m_reader.ReadInt16();
			case Token.Int32:
				return this.m_reader.ReadInt32();
			case Token.Int64:
				return this.m_reader.ReadInt64();
			case Token.UInt16:
				return this.m_reader.ReadUInt16();
			case Token.UInt32:
				return this.m_reader.ReadUInt32();
			case Token.UInt64:
				return this.m_reader.ReadUInt64();
			case Token.Byte:
				return this.m_reader.ReadByte();
			case Token.SByte:
				return this.m_reader.ReadSByte();
			case Token.Single:
				return this.m_reader.ReadSingle();
			case Token.Double:
				return this.m_reader.ReadDouble();
			case Token.Decimal:
				return this.m_reader.ReadDecimal();
			default:
				Global.Tracer.Assert(false, string.Format("IntermediateFormatReader does not support {0}.{1}.", token.GetType(), token));
				return null;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005690 File Offset: 0x00003890
		internal int[] ReadInt32Array()
		{
			return this.m_reader.ReadInt32Array();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000569D File Offset: 0x0000389D
		internal long[] ReadInt64Array()
		{
			return this.m_reader.ReadInt64Array();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000056AA File Offset: 0x000038AA
		internal float[] ReadSingleArray()
		{
			return this.m_reader.ReadFloatArray();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000056B7 File Offset: 0x000038B7
		internal char[] ReadCharArray()
		{
			return this.m_reader.ReadCharArray();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000056C4 File Offset: 0x000038C4
		internal byte[] ReadByteArray()
		{
			return this.m_reader.ReadByteArray();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000056D1 File Offset: 0x000038D1
		internal bool[] ReadBooleanArray()
		{
			return this.m_reader.ReadBooleanArray();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000056DE File Offset: 0x000038DE
		internal double[] ReadDoubleArray()
		{
			return this.m_reader.ReadDoubleArray();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000056EB File Offset: 0x000038EB
		internal byte ReadByte()
		{
			return this.ReadByte(true);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000056F4 File Offset: 0x000038F4
		internal byte ReadByte(bool verify)
		{
			return this.m_reader.ReadByte();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005701 File Offset: 0x00003901
		internal sbyte ReadSByte()
		{
			return this.m_reader.ReadSByte();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000570E File Offset: 0x0000390E
		internal char ReadChar()
		{
			return this.m_reader.ReadChar();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000571B File Offset: 0x0000391B
		internal short ReadInt16()
		{
			return this.m_reader.ReadInt16();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005728 File Offset: 0x00003928
		internal ushort ReadUInt16()
		{
			return this.m_reader.ReadUInt16();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005735 File Offset: 0x00003935
		internal int ReadInt32()
		{
			return this.ReadInt32(true);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000573E File Offset: 0x0000393E
		private int ReadInt32(bool verify)
		{
			return this.m_reader.ReadInt32();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000574B File Offset: 0x0000394B
		internal uint ReadUInt32()
		{
			return this.m_reader.ReadUInt32();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005758 File Offset: 0x00003958
		internal long ReadInt64()
		{
			return this.m_reader.ReadInt64();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005765 File Offset: 0x00003965
		internal ulong ReadUInt64()
		{
			return this.m_reader.ReadUInt64();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005772 File Offset: 0x00003972
		internal float ReadSingle()
		{
			return this.m_reader.ReadSingle();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000577F File Offset: 0x0000397F
		internal double ReadDouble()
		{
			return this.m_reader.ReadDouble();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000578C File Offset: 0x0000398C
		internal decimal ReadDecimal()
		{
			return this.m_reader.ReadDecimal();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005799 File Offset: 0x00003999
		internal string ReadString()
		{
			return this.ReadString(true);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000057A2 File Offset: 0x000039A2
		private string ReadString(bool verify)
		{
			return this.m_reader.ReadString();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000057AF File Offset: 0x000039AF
		internal bool ReadBoolean()
		{
			return this.m_reader.ReadBoolean();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000057BC File Offset: 0x000039BC
		internal DateTime ReadDateTime()
		{
			return this.m_reader.ReadDateTime();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000057C9 File Offset: 0x000039C9
		internal DateTime ReadDateTimeWithKind()
		{
			return this.m_reader.ReadDateTimeWithKind();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000057D6 File Offset: 0x000039D6
		internal DateTimeOffset ReadDateTimeOffset()
		{
			return this.m_reader.ReadDateTimeOffset();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000057E3 File Offset: 0x000039E3
		internal TimeSpan ReadTimeSpan()
		{
			return this.m_reader.ReadTimeSpan();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000057F0 File Offset: 0x000039F0
		internal int Read7BitEncodedInt()
		{
			return this.m_reader.ReadEnum();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000057FD File Offset: 0x000039FD
		internal int ReadEnum()
		{
			return this.m_reader.ReadEnum();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000580A File Offset: 0x00003A0A
		internal Guid ReadGuid()
		{
			return this.m_reader.ReadGuid();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005818 File Offset: 0x00003A18
		internal CultureInfo ReadCultureInfo()
		{
			int num = this.m_reader.ReadInt32();
			if (num == -1)
			{
				return null;
			}
			return new CultureInfo(num, false);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005840 File Offset: 0x00003A40
		internal List<T> ReadGenericListOfReferences<T>(IPersistable obj) where T : IReferenceable
		{
			int num;
			if (this.m_reader.ReadListStart(this.m_currentMember.ObjectType, out num))
			{
				List<T> list = new List<T>();
				for (int i = 0; i < num; i++)
				{
					T t = this.ReadReference<T>(obj, false);
					if (t != null)
					{
						list.Add(t);
					}
				}
				return list;
			}
			return null;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005894 File Offset: 0x00003A94
		internal int ReadListOfReferencesNoResolution(IPersistable obj)
		{
			int num;
			if (this.m_reader.ReadListStart(this.m_currentMember.ObjectType, out num))
			{
				for (int i = 0; i < num; i++)
				{
					this.ReadReference<IReferenceable>(obj, true);
				}
			}
			return num;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000058D4 File Offset: 0x00003AD4
		internal T ReadListOfReferences<T, U>(IPersistable obj) where T : class, IList, new() where U : IReferenceable
		{
			int num;
			if (this.m_reader.ReadListStart(this.m_currentMember.ObjectType, out num))
			{
				T t = new T();
				for (int i = 0; i < num; i++)
				{
					U u = this.ReadReference<U>(obj, false);
					if (u != null)
					{
						t.Add(u);
					}
				}
				return t;
			}
			return default(T);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000593C File Offset: 0x00003B3C
		internal T ReadReference<T>(IPersistable obj) where T : IReferenceable
		{
			return this.ReadReference<T>(obj, false);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005948 File Offset: 0x00003B48
		private T ReadReference<T>(IPersistable obj, bool delayReferenceResolution) where T : IReferenceable
		{
			int num;
			ObjectType objectType;
			if (this.m_reader.ReadReference(out num, out objectType))
			{
				IReferenceable referenceable = null;
				if (delayReferenceResolution || !this.m_referenceableItems.TryGetValue(num, out referenceable))
				{
					Dictionary<ObjectType, List<MemberReference>> dictionary;
					if (!this.m_memberReferencesCollection.TryGetValue(obj, out dictionary))
					{
						dictionary = new Dictionary<ObjectType, List<MemberReference>>(EqualityComparers.ObjectTypeComparerInstance);
						this.m_memberReferencesCollection.Add(obj, dictionary);
					}
					List<MemberReference> list;
					if (!dictionary.TryGetValue(this.m_currentPersistedDeclaration.ObjectType, out list))
					{
						list = new List<MemberReference>();
						dictionary.Add(this.m_currentPersistedDeclaration.ObjectType, list);
					}
					list.Add(new MemberReference(this.m_currentMember.MemberName, num));
				}
				return (T)((object)referenceable);
			}
			return default(T);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005A00 File Offset: 0x00003C00
		internal List<T> ReadGenericListOfGloablReferences<T>() where T : IGloballyReferenceable
		{
			int num;
			if (this.m_reader.ReadListStart(this.m_currentMember.ObjectType, out num))
			{
				List<T> list = new List<T>();
				for (int i = 0; i < num; i++)
				{
					T t = this.ReadGlobalReference<T>();
					if (t != null)
					{
						list.Add(t);
					}
				}
				return list;
			}
			return null;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005A54 File Offset: 0x00003C54
		internal T ReadListOfGloablReferences<T, U>() where T : class, IList, new() where U : IGloballyReferenceable
		{
			int num;
			if (this.m_reader.ReadListStart(this.m_currentMember.ObjectType, out num))
			{
				T t = new T();
				for (int i = 0; i < num; i++)
				{
					U u = this.ReadGlobalReference<U>();
					if (u != null)
					{
						t.Add(u);
					}
				}
				return t;
			}
			return default(T);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005ABC File Offset: 0x00003CBC
		internal T ReadGlobalReference<T>() where T : IGloballyReferenceable
		{
			IGloballyReferenceable globallyReferenceable = null;
			int num;
			ObjectType objectType;
			if (this.m_reader.ReadReference(out num, out objectType))
			{
				this.m_globalIDOwners.TryGetValue(num, out globallyReferenceable);
			}
			return (T)((object)globallyReferenceable);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005AF4 File Offset: 0x00003CF4
		internal IntermediateFormatVersion ReadIntermediateFormatVersion()
		{
			long streamPosition = this.m_reader.StreamPosition;
			ObjectType objectType = this.m_reader.ReadObjectType();
			if (objectType != ObjectType.IntermediateFormatVersion)
			{
				throw new IncompatibleFormatVersionException(objectType, streamPosition);
			}
			IntermediateFormatVersion intermediateFormatVersion = new IntermediateFormatVersion();
			intermediateFormatVersion.Deserialize(this);
			return intermediateFormatVersion;
		}

		// Token: 0x040000D6 RID: 214
		private int m_currentMemberIndex;

		// Token: 0x040000D7 RID: 215
		private Declaration m_currentPersistedDeclaration;

		// Token: 0x040000D8 RID: 216
		private Dictionary<ObjectType, Declaration> m_readDecls;

		// Token: 0x040000D9 RID: 217
		private PersistenceBinaryReader m_reader;

		// Token: 0x040000DA RID: 218
		private Dictionary<IPersistable, Dictionary<ObjectType, List<MemberReference>>> m_memberReferencesCollection;

		// Token: 0x040000DB RID: 219
		private Dictionary<int, IReferenceable> m_referenceableItems;

		// Token: 0x040000DC RID: 220
		private GlobalIDOwnerCollection m_globalIDOwners;

		// Token: 0x040000DD RID: 221
		private IRIFObjectCreator m_rifObjectCreator;

		// Token: 0x040000DE RID: 222
		private PersistenceHelper m_persistenceHelper;

		// Token: 0x040000DF RID: 223
		private IntermediateFormatVersion m_version;

		// Token: 0x040000E0 RID: 224
		private long m_objectStartPosition;

		// Token: 0x040000E1 RID: 225
		private PersistenceFlags m_persistenceFlags;

		// Token: 0x040000E2 RID: 226
		private int m_currentMemberInfoCount;

		// Token: 0x040000E3 RID: 227
		private MemberInfo m_currentMember;

		// Token: 0x040000E4 RID: 228
		private int m_compatVersion;

		// Token: 0x040000E5 RID: 229
		private BinaryFormatter m_binaryFormatter;
	}
}
