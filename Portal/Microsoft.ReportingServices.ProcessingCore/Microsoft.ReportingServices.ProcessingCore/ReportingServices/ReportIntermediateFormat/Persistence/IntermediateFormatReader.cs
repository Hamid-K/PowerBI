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
	// Token: 0x02000534 RID: 1332
	public struct IntermediateFormatReader
	{
		// Token: 0x0600486E RID: 18542 RVA: 0x001323F2 File Offset: 0x001305F2
		internal IntermediateFormatReader(Stream str, IRIFObjectCreator rifObjectCreator)
		{
			this = new IntermediateFormatReader(str, rifObjectCreator, null, null);
		}

		// Token: 0x0600486F RID: 18543 RVA: 0x001323FE File Offset: 0x001305FE
		internal IntermediateFormatReader(Stream str, IRIFObjectCreator rifObjectCreator, PersistenceHelper persistenceHelper)
		{
			this = new IntermediateFormatReader(str, rifObjectCreator, null, persistenceHelper);
		}

		// Token: 0x06004870 RID: 18544 RVA: 0x0013240A File Offset: 0x0013060A
		internal IntermediateFormatReader(Stream str, IRIFObjectCreator rifObjectCreator, GlobalIDOwnerCollection globalIDOwnersFromOtherStream)
		{
			this = new IntermediateFormatReader(str, rifObjectCreator, globalIDOwnersFromOtherStream, null);
		}

		// Token: 0x06004871 RID: 18545 RVA: 0x00132418 File Offset: 0x00130618
		internal IntermediateFormatReader(Stream str, IRIFObjectCreator rifObjectCreator, GlobalIDOwnerCollection globalIDOwnersFromOtherStream, PersistenceHelper persistenceHelper)
		{
			this = new IntermediateFormatReader(str, rifObjectCreator, globalIDOwnersFromOtherStream, persistenceHelper, null, null, PersistenceFlags.None, true, PersistenceConstants.CurrentCompatVersion);
		}

		// Token: 0x06004872 RID: 18546 RVA: 0x0013243C File Offset: 0x0013063C
		internal IntermediateFormatReader(Stream str, IRIFObjectCreator rifObjectCreator, GlobalIDOwnerCollection globalIDOwnersFromOtherStream, PersistenceHelper persistenceHelper, int compatVersion)
		{
			this = new IntermediateFormatReader(str, rifObjectCreator, globalIDOwnersFromOtherStream, persistenceHelper, null, null, PersistenceFlags.None, true, compatVersion);
		}

		// Token: 0x06004873 RID: 18547 RVA: 0x0013245C File Offset: 0x0013065C
		internal IntermediateFormatReader(Stream str, IRIFObjectCreator rifObjectCreator, GlobalIDOwnerCollection globalIDOwnersFromOtherStream, PersistenceHelper persistenceHelper, List<Declaration> declarations, IntermediateFormatVersion version, PersistenceFlags flags)
		{
			this = new IntermediateFormatReader(str, rifObjectCreator, globalIDOwnersFromOtherStream, persistenceHelper, declarations, version, flags, false, PersistenceConstants.CurrentCompatVersion);
		}

		// Token: 0x06004874 RID: 18548 RVA: 0x00132480 File Offset: 0x00130680
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

		// Token: 0x06004875 RID: 18549 RVA: 0x001325B6 File Offset: 0x001307B6
		private static bool HasPersistenceFlag(PersistenceFlags flags, PersistenceFlags flagToTest)
		{
			return (flags & flagToTest) > PersistenceFlags.None;
		}

		// Token: 0x17001DC3 RID: 7619
		// (get) Token: 0x06004876 RID: 18550 RVA: 0x001325BE File Offset: 0x001307BE
		internal bool CanSeek
		{
			get
			{
				return IntermediateFormatReader.HasPersistenceFlag(this.m_persistenceFlags, PersistenceFlags.Seekable);
			}
		}

		// Token: 0x17001DC4 RID: 7620
		// (get) Token: 0x06004877 RID: 18551 RVA: 0x001325CC File Offset: 0x001307CC
		internal bool EOS
		{
			get
			{
				return this.m_reader.EOS;
			}
		}

		// Token: 0x17001DC5 RID: 7621
		// (get) Token: 0x06004878 RID: 18552 RVA: 0x001325D9 File Offset: 0x001307D9
		internal IntermediateFormatVersion IntermediateFormatVersion
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x17001DC6 RID: 7622
		// (get) Token: 0x06004879 RID: 18553 RVA: 0x001325E1 File Offset: 0x001307E1
		internal MemberInfo CurrentMember
		{
			get
			{
				return this.m_currentMember;
			}
		}

		// Token: 0x17001DC7 RID: 7623
		// (get) Token: 0x0600487A RID: 18554 RVA: 0x001325E9 File Offset: 0x001307E9
		internal PersistenceHelper PersistenceHelper
		{
			get
			{
				return this.m_persistenceHelper;
			}
		}

		// Token: 0x17001DC8 RID: 7624
		// (get) Token: 0x0600487B RID: 18555 RVA: 0x001325F1 File Offset: 0x001307F1
		internal long ObjectStartPosition
		{
			get
			{
				return this.m_objectStartPosition;
			}
		}

		// Token: 0x17001DC9 RID: 7625
		// (get) Token: 0x0600487C RID: 18556 RVA: 0x001325F9 File Offset: 0x001307F9
		internal bool HasReferences
		{
			get
			{
				return this.m_memberReferencesCollection != null && this.m_memberReferencesCollection.Count > 0;
			}
		}

		// Token: 0x17001DCA RID: 7626
		// (get) Token: 0x0600487D RID: 18557 RVA: 0x00132613 File Offset: 0x00130813
		internal GlobalIDOwnerCollection GlobalIDOwners
		{
			get
			{
				return this.m_globalIDOwners;
			}
		}

		// Token: 0x0600487E RID: 18558 RVA: 0x0013261C File Offset: 0x0013081C
		private void PrepareDeclarationsFromInitialization(List<Declaration> declarations)
		{
			for (int i = 0; i < declarations.Count; i++)
			{
				Declaration declaration = declarations[i].CreateFilteredDeclarationForWriteVersion(this.m_compatVersion);
				this.m_readDecls.Add(declaration.ObjectType, declaration);
			}
		}

		// Token: 0x0600487F RID: 18559 RVA: 0x00132660 File Offset: 0x00130860
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

		// Token: 0x06004880 RID: 18560 RVA: 0x001326AC File Offset: 0x001308AC
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

		// Token: 0x06004881 RID: 18561 RVA: 0x00132740 File Offset: 0x00130940
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

		// Token: 0x06004882 RID: 18562 RVA: 0x001327C8 File Offset: 0x001309C8
		internal void ResolveReferences()
		{
			foreach (KeyValuePair<IPersistable, Dictionary<ObjectType, List<MemberReference>>> keyValuePair in this.m_memberReferencesCollection)
			{
				keyValuePair.Key.ResolveReferences(keyValuePair.Value, this.m_referenceableItems);
				keyValuePair.Value.Clear();
			}
		}

		// Token: 0x06004883 RID: 18563 RVA: 0x0013283C File Offset: 0x00130A3C
		internal void ClearReferences()
		{
			this.m_referenceableItems = new Dictionary<int, IReferenceable>(EqualityComparers.Int32ComparerInstance);
			this.m_memberReferencesCollection = new Dictionary<IPersistable, Dictionary<ObjectType, List<MemberReference>>>();
		}

		// Token: 0x06004884 RID: 18564 RVA: 0x0013285C File Offset: 0x00130A5C
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

		// Token: 0x06004885 RID: 18565 RVA: 0x00132A90 File Offset: 0x00130C90
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

		// Token: 0x06004886 RID: 18566 RVA: 0x00132B84 File Offset: 0x00130D84
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

		// Token: 0x06004887 RID: 18567 RVA: 0x00132BC8 File Offset: 0x00130DC8
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

		// Token: 0x06004888 RID: 18568 RVA: 0x00132C0C File Offset: 0x00130E0C
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

		// Token: 0x06004889 RID: 18569 RVA: 0x00132C58 File Offset: 0x00130E58
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

		// Token: 0x0600488A RID: 18570 RVA: 0x00132C94 File Offset: 0x00130E94
		private void SkipRIFObject()
		{
			ObjectType objectType = this.m_reader.ReadObjectType();
			if (objectType != ObjectType.Null)
			{
				((IntermediateFormatReader)base.MemberwiseClone()).__SkipRIFObjectPrivate(objectType);
			}
		}

		// Token: 0x0600488B RID: 18571 RVA: 0x00132CD0 File Offset: 0x00130ED0
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

		// Token: 0x0600488C RID: 18572 RVA: 0x00132D40 File Offset: 0x00130F40
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

		// Token: 0x0600488D RID: 18573 RVA: 0x00132D84 File Offset: 0x00130F84
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

		// Token: 0x0600488E RID: 18574 RVA: 0x00132DCC File Offset: 0x00130FCC
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

		// Token: 0x0600488F RID: 18575 RVA: 0x00132E14 File Offset: 0x00131014
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

		// Token: 0x06004890 RID: 18576 RVA: 0x00132E64 File Offset: 0x00131064
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

		// Token: 0x06004891 RID: 18577 RVA: 0x00132EAE File Offset: 0x001310AE
		internal void Seek(long newPosition)
		{
			this.Seek(newPosition, SeekOrigin.Begin);
		}

		// Token: 0x06004892 RID: 18578 RVA: 0x00132EB8 File Offset: 0x001310B8
		internal void Seek(long newPosition, SeekOrigin seekOrigin)
		{
			this.m_reader.Seek(newPosition, seekOrigin);
		}

		// Token: 0x06004893 RID: 18579 RVA: 0x00132EC7 File Offset: 0x001310C7
		internal IPersistable ReadRIFObject()
		{
			return this.ReadRIFObject(true);
		}

		// Token: 0x06004894 RID: 18580 RVA: 0x00132ED0 File Offset: 0x001310D0
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

		// Token: 0x06004895 RID: 18581 RVA: 0x00132F04 File Offset: 0x00131104
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

		// Token: 0x06004896 RID: 18582 RVA: 0x00132F4C File Offset: 0x0013114C
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

		// Token: 0x06004897 RID: 18583 RVA: 0x00132F7C File Offset: 0x0013117C
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

		// Token: 0x06004898 RID: 18584 RVA: 0x00132FE2 File Offset: 0x001311E2
		private ObjectType ReadRIFObjectStart()
		{
			this.m_objectStartPosition = this.m_reader.StreamPosition;
			return this.m_reader.ReadObjectType();
		}

		// Token: 0x06004899 RID: 18585 RVA: 0x00133000 File Offset: 0x00131200
		private void ReadRIFObjectFinish(ObjectType persistedType, IPersistable persitObj, bool verify)
		{
			bool flag = this.m_currentPersistedDeclaration != null && verify;
			if (persitObj is IReferenceable)
			{
				IReferenceable referenceable = (IReferenceable)persitObj;
				this.m_referenceableItems.Add(referenceable.ID, referenceable);
			}
		}

		// Token: 0x0600489A RID: 18586 RVA: 0x0013303C File Offset: 0x0013123C
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

		// Token: 0x0600489B RID: 18587 RVA: 0x00133098 File Offset: 0x00131298
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

		// Token: 0x0600489C RID: 18588 RVA: 0x001330F4 File Offset: 0x001312F4
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

		// Token: 0x0600489D RID: 18589 RVA: 0x00133150 File Offset: 0x00131350
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

		// Token: 0x0600489E RID: 18590 RVA: 0x001331B4 File Offset: 0x001313B4
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

		// Token: 0x0600489F RID: 18591 RVA: 0x00133230 File Offset: 0x00131430
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

		// Token: 0x060048A0 RID: 18592 RVA: 0x001332AC File Offset: 0x001314AC
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

		// Token: 0x060048A1 RID: 18593 RVA: 0x00133324 File Offset: 0x00131524
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

		// Token: 0x060048A2 RID: 18594 RVA: 0x0013339C File Offset: 0x0013159C
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

		// Token: 0x060048A3 RID: 18595 RVA: 0x00133414 File Offset: 0x00131614
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

		// Token: 0x060048A4 RID: 18596 RVA: 0x00133464 File Offset: 0x00131664
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

		// Token: 0x060048A5 RID: 18597 RVA: 0x001334FC File Offset: 0x001316FC
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

		// Token: 0x060048A6 RID: 18598 RVA: 0x0013354C File Offset: 0x0013174C
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

		// Token: 0x060048A7 RID: 18599 RVA: 0x001335EC File Offset: 0x001317EC
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

		// Token: 0x060048A8 RID: 18600 RVA: 0x00133648 File Offset: 0x00131848
		internal T? ReadNullable<T>() where T : struct
		{
			return (T?)this.ReadVariant();
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x00133655 File Offset: 0x00131855
		internal Dictionary<T, string> ReadRIFObjectStringHashtable<T>() where T : IPersistable
		{
			return this.ReadRIFObjectStringHashtable<T>(null);
		}

		// Token: 0x060048AA RID: 18602 RVA: 0x00133660 File Offset: 0x00131860
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

		// Token: 0x060048AB RID: 18603 RVA: 0x001336B8 File Offset: 0x001318B8
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

		// Token: 0x060048AC RID: 18604 RVA: 0x00133708 File Offset: 0x00131908
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

		// Token: 0x060048AD RID: 18605 RVA: 0x00133758 File Offset: 0x00131958
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

		// Token: 0x060048AE RID: 18606 RVA: 0x001337A8 File Offset: 0x001319A8
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

		// Token: 0x060048AF RID: 18607 RVA: 0x001337FC File Offset: 0x001319FC
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

		// Token: 0x060048B0 RID: 18608 RVA: 0x00133854 File Offset: 0x00131A54
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

		// Token: 0x060048B1 RID: 18609 RVA: 0x001338CC File Offset: 0x00131ACC
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

		// Token: 0x060048B2 RID: 18610 RVA: 0x00133948 File Offset: 0x00131B48
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

		// Token: 0x060048B3 RID: 18611 RVA: 0x00133998 File Offset: 0x00131B98
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

		// Token: 0x060048B4 RID: 18612 RVA: 0x001339F0 File Offset: 0x00131BF0
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

		// Token: 0x060048B5 RID: 18613 RVA: 0x00133A30 File Offset: 0x00131C30
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

		// Token: 0x060048B6 RID: 18614 RVA: 0x00133A74 File Offset: 0x00131C74
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

		// Token: 0x060048B7 RID: 18615 RVA: 0x00133AC4 File Offset: 0x00131CC4
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

		// Token: 0x060048B8 RID: 18616 RVA: 0x00133B10 File Offset: 0x00131D10
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

		// Token: 0x060048B9 RID: 18617 RVA: 0x00133B68 File Offset: 0x00131D68
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

		// Token: 0x060048BA RID: 18618 RVA: 0x00133BB4 File Offset: 0x00131DB4
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

		// Token: 0x060048BB RID: 18619 RVA: 0x00133C00 File Offset: 0x00131E00
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

		// Token: 0x060048BC RID: 18620 RVA: 0x00133C44 File Offset: 0x00131E44
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

		// Token: 0x060048BD RID: 18621 RVA: 0x00133C84 File Offset: 0x00131E84
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

		// Token: 0x060048BE RID: 18622 RVA: 0x00133CDC File Offset: 0x00131EDC
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

		// Token: 0x060048BF RID: 18623 RVA: 0x00133D18 File Offset: 0x00131F18
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

		// Token: 0x060048C0 RID: 18624 RVA: 0x00133D54 File Offset: 0x00131F54
		internal T[] ReadArrayOfRIFObjects<T>() where T : IPersistable
		{
			return this.ReadArrayOfRIFObjects<T>(true);
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x00133D60 File Offset: 0x00131F60
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

		// Token: 0x060048C2 RID: 18626 RVA: 0x00133DA8 File Offset: 0x00131FA8
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

		// Token: 0x060048C3 RID: 18627 RVA: 0x00133E04 File Offset: 0x00132004
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

		// Token: 0x060048C4 RID: 18628 RVA: 0x00133E64 File Offset: 0x00132064
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

		// Token: 0x060048C5 RID: 18629 RVA: 0x00133EB0 File Offset: 0x001320B0
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

		// Token: 0x060048C6 RID: 18630 RVA: 0x00133EF8 File Offset: 0x001320F8
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

		// Token: 0x060048C7 RID: 18631 RVA: 0x00133F40 File Offset: 0x00132140
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

		// Token: 0x060048C8 RID: 18632 RVA: 0x00133F88 File Offset: 0x00132188
		internal object ReadSerializable()
		{
			Token token = this.m_reader.ReadToken();
			if (token == Token.Serializable)
			{
				return this.ReadISerializable();
			}
			return this.ReadVariant(token);
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x00133FB8 File Offset: 0x001321B8
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

		// Token: 0x060048CA RID: 18634 RVA: 0x0013402C File Offset: 0x0013222C
		internal object ReadVariant()
		{
			Token token = this.m_reader.ReadToken();
			return this.ReadVariant(token);
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x0013404C File Offset: 0x0013224C
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

		// Token: 0x060048CC RID: 18636 RVA: 0x00134260 File Offset: 0x00132460
		internal int[] ReadInt32Array()
		{
			return this.m_reader.ReadInt32Array();
		}

		// Token: 0x060048CD RID: 18637 RVA: 0x0013426D File Offset: 0x0013246D
		internal long[] ReadInt64Array()
		{
			return this.m_reader.ReadInt64Array();
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x0013427A File Offset: 0x0013247A
		internal float[] ReadSingleArray()
		{
			return this.m_reader.ReadFloatArray();
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x00134287 File Offset: 0x00132487
		internal char[] ReadCharArray()
		{
			return this.m_reader.ReadCharArray();
		}

		// Token: 0x060048D0 RID: 18640 RVA: 0x00134294 File Offset: 0x00132494
		internal byte[] ReadByteArray()
		{
			return this.m_reader.ReadByteArray();
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x001342A1 File Offset: 0x001324A1
		internal bool[] ReadBooleanArray()
		{
			return this.m_reader.ReadBooleanArray();
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x001342AE File Offset: 0x001324AE
		internal double[] ReadDoubleArray()
		{
			return this.m_reader.ReadDoubleArray();
		}

		// Token: 0x060048D3 RID: 18643 RVA: 0x001342BB File Offset: 0x001324BB
		internal byte ReadByte()
		{
			return this.ReadByte(true);
		}

		// Token: 0x060048D4 RID: 18644 RVA: 0x001342C4 File Offset: 0x001324C4
		internal byte ReadByte(bool verify)
		{
			return this.m_reader.ReadByte();
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x001342D1 File Offset: 0x001324D1
		internal sbyte ReadSByte()
		{
			return this.m_reader.ReadSByte();
		}

		// Token: 0x060048D6 RID: 18646 RVA: 0x001342DE File Offset: 0x001324DE
		internal char ReadChar()
		{
			return this.m_reader.ReadChar();
		}

		// Token: 0x060048D7 RID: 18647 RVA: 0x001342EB File Offset: 0x001324EB
		internal short ReadInt16()
		{
			return this.m_reader.ReadInt16();
		}

		// Token: 0x060048D8 RID: 18648 RVA: 0x001342F8 File Offset: 0x001324F8
		internal ushort ReadUInt16()
		{
			return this.m_reader.ReadUInt16();
		}

		// Token: 0x060048D9 RID: 18649 RVA: 0x00134305 File Offset: 0x00132505
		internal int ReadInt32()
		{
			return this.ReadInt32(true);
		}

		// Token: 0x060048DA RID: 18650 RVA: 0x0013430E File Offset: 0x0013250E
		private int ReadInt32(bool verify)
		{
			return this.m_reader.ReadInt32();
		}

		// Token: 0x060048DB RID: 18651 RVA: 0x0013431B File Offset: 0x0013251B
		internal uint ReadUInt32()
		{
			return this.m_reader.ReadUInt32();
		}

		// Token: 0x060048DC RID: 18652 RVA: 0x00134328 File Offset: 0x00132528
		internal long ReadInt64()
		{
			return this.m_reader.ReadInt64();
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x00134335 File Offset: 0x00132535
		internal ulong ReadUInt64()
		{
			return this.m_reader.ReadUInt64();
		}

		// Token: 0x060048DE RID: 18654 RVA: 0x00134342 File Offset: 0x00132542
		internal float ReadSingle()
		{
			return this.m_reader.ReadSingle();
		}

		// Token: 0x060048DF RID: 18655 RVA: 0x0013434F File Offset: 0x0013254F
		internal double ReadDouble()
		{
			return this.m_reader.ReadDouble();
		}

		// Token: 0x060048E0 RID: 18656 RVA: 0x0013435C File Offset: 0x0013255C
		internal decimal ReadDecimal()
		{
			return this.m_reader.ReadDecimal();
		}

		// Token: 0x060048E1 RID: 18657 RVA: 0x00134369 File Offset: 0x00132569
		internal string ReadString()
		{
			return this.ReadString(true);
		}

		// Token: 0x060048E2 RID: 18658 RVA: 0x00134372 File Offset: 0x00132572
		private string ReadString(bool verify)
		{
			return this.m_reader.ReadString();
		}

		// Token: 0x060048E3 RID: 18659 RVA: 0x0013437F File Offset: 0x0013257F
		internal bool ReadBoolean()
		{
			return this.m_reader.ReadBoolean();
		}

		// Token: 0x060048E4 RID: 18660 RVA: 0x0013438C File Offset: 0x0013258C
		internal DateTime ReadDateTime()
		{
			return this.m_reader.ReadDateTime();
		}

		// Token: 0x060048E5 RID: 18661 RVA: 0x00134399 File Offset: 0x00132599
		internal DateTime ReadDateTimeWithKind()
		{
			return this.m_reader.ReadDateTimeWithKind();
		}

		// Token: 0x060048E6 RID: 18662 RVA: 0x001343A6 File Offset: 0x001325A6
		internal DateTimeOffset ReadDateTimeOffset()
		{
			return this.m_reader.ReadDateTimeOffset();
		}

		// Token: 0x060048E7 RID: 18663 RVA: 0x001343B3 File Offset: 0x001325B3
		internal TimeSpan ReadTimeSpan()
		{
			return this.m_reader.ReadTimeSpan();
		}

		// Token: 0x060048E8 RID: 18664 RVA: 0x001343C0 File Offset: 0x001325C0
		internal int Read7BitEncodedInt()
		{
			return this.m_reader.ReadEnum();
		}

		// Token: 0x060048E9 RID: 18665 RVA: 0x001343CD File Offset: 0x001325CD
		internal int ReadEnum()
		{
			return this.m_reader.ReadEnum();
		}

		// Token: 0x060048EA RID: 18666 RVA: 0x001343DA File Offset: 0x001325DA
		internal Guid ReadGuid()
		{
			return this.m_reader.ReadGuid();
		}

		// Token: 0x060048EB RID: 18667 RVA: 0x001343E8 File Offset: 0x001325E8
		internal CultureInfo ReadCultureInfo()
		{
			int num = this.m_reader.ReadInt32();
			if (num == -1)
			{
				return null;
			}
			return new CultureInfo(num, false);
		}

		// Token: 0x060048EC RID: 18668 RVA: 0x00134410 File Offset: 0x00132610
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

		// Token: 0x060048ED RID: 18669 RVA: 0x00134464 File Offset: 0x00132664
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

		// Token: 0x060048EE RID: 18670 RVA: 0x001344A4 File Offset: 0x001326A4
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

		// Token: 0x060048EF RID: 18671 RVA: 0x0013450C File Offset: 0x0013270C
		internal T ReadReference<T>(IPersistable obj) where T : IReferenceable
		{
			return this.ReadReference<T>(obj, false);
		}

		// Token: 0x060048F0 RID: 18672 RVA: 0x00134518 File Offset: 0x00132718
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

		// Token: 0x060048F1 RID: 18673 RVA: 0x001345D0 File Offset: 0x001327D0
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

		// Token: 0x060048F2 RID: 18674 RVA: 0x00134624 File Offset: 0x00132824
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

		// Token: 0x060048F3 RID: 18675 RVA: 0x0013468C File Offset: 0x0013288C
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

		// Token: 0x060048F4 RID: 18676 RVA: 0x001346C4 File Offset: 0x001328C4
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

		// Token: 0x04002050 RID: 8272
		private int m_currentMemberIndex;

		// Token: 0x04002051 RID: 8273
		private Declaration m_currentPersistedDeclaration;

		// Token: 0x04002052 RID: 8274
		private Dictionary<ObjectType, Declaration> m_readDecls;

		// Token: 0x04002053 RID: 8275
		private PersistenceBinaryReader m_reader;

		// Token: 0x04002054 RID: 8276
		private Dictionary<IPersistable, Dictionary<ObjectType, List<MemberReference>>> m_memberReferencesCollection;

		// Token: 0x04002055 RID: 8277
		private Dictionary<int, IReferenceable> m_referenceableItems;

		// Token: 0x04002056 RID: 8278
		private GlobalIDOwnerCollection m_globalIDOwners;

		// Token: 0x04002057 RID: 8279
		private IRIFObjectCreator m_rifObjectCreator;

		// Token: 0x04002058 RID: 8280
		private PersistenceHelper m_persistenceHelper;

		// Token: 0x04002059 RID: 8281
		private IntermediateFormatVersion m_version;

		// Token: 0x0400205A RID: 8282
		private long m_objectStartPosition;

		// Token: 0x0400205B RID: 8283
		private PersistenceFlags m_persistenceFlags;

		// Token: 0x0400205C RID: 8284
		private int m_currentMemberInfoCount;

		// Token: 0x0400205D RID: 8285
		private MemberInfo m_currentMember;

		// Token: 0x0400205E RID: 8286
		private int m_compatVersion;

		// Token: 0x0400205F RID: 8287
		private BinaryFormatter m_binaryFormatter;
	}
}
