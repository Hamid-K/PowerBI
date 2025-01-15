using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using Microsoft.SqlServer.Server;

// Token: 0x02000003 RID: 3
[SqlUserDefinedAggregate(2, MaxByteSize = -1, IsInvariantToDuplicates = false, IsInvariantToNulls = true, IsInvariantToOrder = true, Name = "TransformationAggregate", IsNullIfEmpty = false)]
[Serializable]
public class TransformationAggregate : IBinarySerialize
{
	// Token: 0x06000008 RID: 8 RVA: 0x00002273 File Offset: 0x00000473
	public void Init()
	{
		this.m_toTokens = new List<int>();
		this.m_metadata = new List<byte[]>();
	}

	// Token: 0x06000009 RID: 9 RVA: 0x0000228C File Offset: 0x0000048C
	public void Accumulate([SqlFacet(IsNullable = true)] SqlInt32 toTokenId, [SqlFacet(IsNullable = true)] SqlBinary metadata)
	{
		if (!toTokenId.IsNull)
		{
			this.m_toTokens.Add(toTokenId.Value);
			if (!metadata.IsNull)
			{
				this.m_metadata.Add(metadata.Value);
				return;
			}
			this.m_metadata.Add(null);
			return;
		}
		else
		{
			if (!metadata.IsNull)
			{
				throw new Exception("Metadata may not be non-null when toTokenId is null.");
			}
			return;
		}
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000022F1 File Offset: 0x000004F1
	public void Merge(TransformationAggregate Group)
	{
		this.m_toTokens.AddRange(Group.m_toTokens);
		this.m_metadata.AddRange(Group.m_metadata);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002318 File Offset: 0x00000518
	public SqlBytes Terminate()
	{
		MemoryStream memoryStream = new MemoryStream();
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		this.Write(binaryWriter);
		binaryWriter.Flush();
		binaryWriter.BaseStream.Flush();
		memoryStream.SetLength(memoryStream.Position);
		memoryStream.Seek(0L, 0);
		return new SqlBytes(memoryStream);
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002364 File Offset: 0x00000564
	public void Read(BinaryReader r)
	{
		int num = r.ReadInt32();
		this.m_toTokens = new List<int>(num);
		this.m_metadata = new List<byte[]>(num);
		for (int i = 0; i < num; i++)
		{
			this.m_toTokens.Add(r.ReadInt32());
			int num2 = r.ReadInt32();
			if (num2 > 0)
			{
				this.m_metadata.Add(r.ReadBytes(num2));
			}
			else
			{
				this.m_metadata.Add(null);
			}
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000023D8 File Offset: 0x000005D8
	public void Write(BinaryWriter w)
	{
		w.Write(this.m_toTokens.Count);
		for (int i = 0; i < this.m_toTokens.Count; i++)
		{
			w.Write(this.m_toTokens[i]);
			if (this.m_metadata[i] != null)
			{
				w.Write(this.m_metadata[i].Length);
				w.Write(this.m_metadata[i]);
			}
			else
			{
				w.Write(0);
			}
		}
	}

	// Token: 0x04000004 RID: 4
	public List<int> m_toTokens;

	// Token: 0x04000005 RID: 5
	public List<byte[]> m_metadata;
}
