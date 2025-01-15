using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x020000C2 RID: 194
	public abstract class ODataWriter
	{
		// Token: 0x06000870 RID: 2160
		public abstract void WriteStart(ODataResourceSet resourceSet);

		// Token: 0x06000871 RID: 2161 RVA: 0x00013F40 File Offset: 0x00012140
		public ODataWriter Write(ODataResourceSet resourceSet)
		{
			this.WriteStart(resourceSet);
			this.WriteEnd();
			return this;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00013F50 File Offset: 0x00012150
		public ODataWriter Write(ODataResourceSet resourceSet, Action nestedAction)
		{
			this.WriteStart(resourceSet);
			nestedAction();
			this.WriteEnd();
			return this;
		}

		// Token: 0x06000873 RID: 2163
		public abstract Task WriteStartAsync(ODataResourceSet resourceSet);

		// Token: 0x06000874 RID: 2164 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void WriteStart(ODataDeltaResourceSet deltaResourceSet)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00013F66 File Offset: 0x00012166
		public ODataWriter Write(ODataDeltaResourceSet deltaResourceSet)
		{
			this.WriteStart(deltaResourceSet);
			this.WriteEnd();
			return this;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00013F76 File Offset: 0x00012176
		public ODataWriter Write(ODataDeltaResourceSet deltaResourceSet, Action nestedAction)
		{
			this.WriteStart(deltaResourceSet);
			nestedAction();
			this.WriteEnd();
			return this;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual Task WriteStartAsync(ODataDeltaResourceSet deltaResourceSet)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000878 RID: 2168
		public abstract void WriteStart(ODataResource resource);

		// Token: 0x06000879 RID: 2169 RVA: 0x00013F8C File Offset: 0x0001218C
		public ODataWriter Write(ODataResource resource)
		{
			this.WriteStart(resource);
			this.WriteEnd();
			return this;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00013F9C File Offset: 0x0001219C
		public ODataWriter Write(ODataResource resource, Action nestedAction)
		{
			this.WriteStart(resource);
			nestedAction();
			this.WriteEnd();
			return this;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00013FB2 File Offset: 0x000121B2
		public ODataWriter Write(ODataDeletedResource deletedResource)
		{
			this.WriteStart(deletedResource);
			this.WriteEnd();
			return this;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00013FC2 File Offset: 0x000121C2
		public ODataWriter Write(ODataDeletedResource deletedResource, Action nestedAction)
		{
			this.WriteStart(deletedResource);
			nestedAction();
			this.WriteEnd();
			return this;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00013FD8 File Offset: 0x000121D8
		public ODataWriter Write(ODataDeltaLink deltaLink)
		{
			this.WriteDeltaLink(deltaLink);
			return this;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00013FE2 File Offset: 0x000121E2
		public ODataWriter Write(ODataDeltaDeletedLink deltaDeletedLink)
		{
			this.WriteDeltaDeletedLink(deltaDeletedLink);
			return this;
		}

		// Token: 0x0600087F RID: 2175
		public abstract Task WriteStartAsync(ODataResource resource);

		// Token: 0x06000880 RID: 2176 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void WriteStart(ODataDeletedResource deletedResource)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00013FEC File Offset: 0x000121EC
		public virtual Task WriteStartAsync(ODataDeletedResource deletedResource)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteStart(deletedResource);
			});
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void WriteDeltaLink(ODataDeltaLink deltaLink)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00014020 File Offset: 0x00012220
		public virtual Task WriteDeltaLinkAsync(ODataDeltaLink deltaLink)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteDeltaLink(deltaLink);
			});
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void WriteDeltaDeletedLink(ODataDeltaDeletedLink deltaDeletedLink)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00014054 File Offset: 0x00012254
		public virtual Task WriteDeltaDeletedLinkAsync(ODataDeltaDeletedLink deltaDeletedLink)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteDeltaDeletedLink(deltaDeletedLink);
			});
		}

		// Token: 0x06000886 RID: 2182
		public abstract void WriteStart(ODataNestedResourceInfo nestedResourceInfo);

		// Token: 0x06000887 RID: 2183 RVA: 0x00014086 File Offset: 0x00012286
		public ODataWriter Write(ODataNestedResourceInfo nestedResourceInfo)
		{
			this.WriteStart(nestedResourceInfo);
			this.WriteEnd();
			return this;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00014096 File Offset: 0x00012296
		public ODataWriter Write(ODataNestedResourceInfo nestedResourceInfo, Action nestedAction)
		{
			this.WriteStart(nestedResourceInfo);
			nestedAction();
			this.WriteEnd();
			return this;
		}

		// Token: 0x06000889 RID: 2185
		public abstract Task WriteStartAsync(ODataNestedResourceInfo nestedResourceInfo);

		// Token: 0x0600088A RID: 2186 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void WritePrimitive(ODataPrimitiveValue primitiveValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x000140AC File Offset: 0x000122AC
		public ODataWriter Write(ODataPrimitiveValue primitiveValue)
		{
			this.WritePrimitive(primitiveValue);
			return this;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x000140B8 File Offset: 0x000122B8
		public virtual Task WritePrimitiveAsync(ODataPrimitiveValue primitiveValue)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WritePrimitive(primitiveValue);
			});
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void WriteStart(ODataPropertyInfo primitiveProperty)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x000140EA File Offset: 0x000122EA
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public ODataWriter Write(ODataProperty primitiveProperty)
		{
			this.WriteStart(primitiveProperty);
			this.WriteEnd();
			return this;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000140FA File Offset: 0x000122FA
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public ODataWriter Write(ODataProperty primitiveProperty, Action nestedAction)
		{
			this.WriteStart(primitiveProperty);
			nestedAction();
			this.WriteEnd();
			return this;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00014110 File Offset: 0x00012310
		public virtual Task WriteStartAsync(ODataProperty primitiveProperty)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteStart(primitiveProperty);
			});
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual Stream CreateBinaryWriteStream()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00014144 File Offset: 0x00012344
		public ODataWriter WriteStream(ODataBinaryStreamValue stream)
		{
			Stream stream2 = this.CreateBinaryWriteStream();
			stream.Stream.CopyTo(stream2);
			stream2.Flush();
			stream2.Dispose();
			return this;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00014171 File Offset: 0x00012371
		public virtual Task<Stream> CreateBinaryWriteStreamAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<Stream>(() => this.CreateBinaryWriteStream());
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual TextWriter CreateTextWriter()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00014184 File Offset: 0x00012384
		public virtual Task<TextWriter> CreateTextWriterAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<TextWriter>(() => this.CreateTextWriter());
		}

		// Token: 0x06000896 RID: 2198
		public abstract void WriteEnd();

		// Token: 0x06000897 RID: 2199
		public abstract Task WriteEndAsync();

		// Token: 0x06000898 RID: 2200
		public abstract void WriteEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink);

		// Token: 0x06000899 RID: 2201
		public abstract Task WriteEntityReferenceLinkAsync(ODataEntityReferenceLink entityReferenceLink);

		// Token: 0x0600089A RID: 2202
		public abstract void Flush();

		// Token: 0x0600089B RID: 2203
		public abstract Task FlushAsync();
	}
}
