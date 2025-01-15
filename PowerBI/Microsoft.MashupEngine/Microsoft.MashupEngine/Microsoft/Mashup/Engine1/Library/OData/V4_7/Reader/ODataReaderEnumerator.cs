using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.Spatial;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader
{
	// Token: 0x020007A1 RID: 1953
	internal sealed class ODataReaderEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
	{
		// Token: 0x06003929 RID: 14633 RVA: 0x000B7E34 File Offset: 0x000B6034
		public ODataReaderEnumerator(ODataEnvironment environment, GetReader getReader, ODataReaderWithResponse reader, TypeValue itemType, bool isResourceSet, Microsoft.OData.Edm.IEdmNavigationSource navigationSource = null)
		{
			this.environment = environment;
			this.getReader = getReader;
			this.itemType = itemType;
			this.isResourceSet = isResourceSet;
			this.converter = new ODataResourceValueConverter(this.environment, getReader);
			if (navigationSource != null)
			{
				this.rootBindingPath = ODataBindingPath.RootOf(navigationSource);
			}
			this.completed = false;
			this.InitializeCurrentReader(reader);
		}

		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x0600392A RID: 14634 RVA: 0x000B7E96 File Offset: 0x000B6096
		public IValueReference Current
		{
			get
			{
				return this.currentValue;
			}
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x000B7E9E File Offset: 0x000B609E
		public void Dispose()
		{
			this.completed = true;
			this.DisposeCurrentReader();
		}

		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x0600392C RID: 14636 RVA: 0x000B7EAD File Offset: 0x000B60AD
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x000091AE File Offset: 0x000073AE
		public void Reset()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x000B7EB8 File Offset: 0x000B60B8
		public bool MoveNext()
		{
			if (this.completed)
			{
				return false;
			}
			if (this.IsNull())
			{
				this.DisposeCurrentReader();
				this.currentValue = Value.Null;
				this.completed = true;
				return true;
			}
			if (this.currentReader.State == ODataReaderState.Start && !this.Start())
			{
				return false;
			}
			while (this.currentReader.State == ODataReaderState.ResourceSetEnd)
			{
				Uri nextPageLink = this.Read<ODataResourceSet>(ODataReaderState.ResourceSetEnd).NextPageLink;
				if (nextPageLink == null)
				{
					this.Complete();
					return false;
				}
				this.DisposeCurrentReader();
				using (IODataPayloadReader value = this.getReader(new GetReaderArgs
				{
					Uri = nextPageLink,
					Catch404 = false
				}).Value)
				{
					this.InitializeCurrentReader(value.ToResourceReader(this.isResourceSet));
				}
				if (!this.Start())
				{
					this.Complete();
					return false;
				}
			}
			if (this.currentReader.State == ODataReaderState.Completed)
			{
				this.Complete();
				return false;
			}
			if (this.currentReader.State == ODataReaderState.Primitive)
			{
				this.currentValue = this.ReadPrimitive();
				return true;
			}
			this.VerifyState(ODataReaderState.ResourceStart);
			ODataNestedResource? odataNestedResource = this.ReadResource();
			this.currentValue = ((odataNestedResource != null) ? this.CreateResourceValue(odataNestedResource.Value, this.itemType, this.rootBindingPath) : Value.Null);
			return true;
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x000B8014 File Offset: 0x000B6214
		private bool Start()
		{
			this.Read(ODataReaderState.Start);
			if (this.currentReader.State == ODataReaderState.Completed)
			{
				this.Complete();
				return false;
			}
			if (this.isResourceSet)
			{
				this.Read(ODataReaderState.ResourceSetStart);
			}
			return true;
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x000B8044 File Offset: 0x000B6244
		private void Complete()
		{
			this.Read(ODataReaderState.Completed);
			this.DisposeCurrentReader();
			this.completed = true;
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x000B805C File Offset: 0x000B625C
		private ODataNestedResource? ReadResource()
		{
			this.Read(ODataReaderState.ResourceStart);
			List<KeyValuePair<ODataNestedResourceInfoWrapper, ODataNestedValues>> list = new List<KeyValuePair<ODataNestedResourceInfoWrapper, ODataNestedValues>>();
			while (this.currentReader.State == ODataReaderState.NestedResourceInfoStart)
			{
				list.Add(this.ReadNestedResourceInfo());
			}
			ODataResource odataResource = this.Read<ODataResource>(ODataReaderState.ResourceEnd);
			if (odataResource == null)
			{
				return null;
			}
			return new ODataNestedResource?(new ODataNestedResource(odataResource, list));
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000B80B3 File Offset: 0x000B62B3
		private Value ReadPrimitive()
		{
			return ODataTypeServices.MarshalFromClr(this.Read<ODataPrimitiveValue>(ODataReaderState.Primitive).Value);
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x000B80C8 File Offset: 0x000B62C8
		private KeyValuePair<ODataNestedResourceInfoWrapper, ODataNestedValues> ReadNestedResourceInfo()
		{
			this.Read(ODataReaderState.NestedResourceInfoStart);
			List<ODataNestedValue> list = null;
			Uri uri = null;
			ODataReaderState state = this.currentReader.State;
			if (state != ODataReaderState.ResourceSetStart)
			{
				if (state == ODataReaderState.ResourceStart)
				{
					list = new List<ODataNestedValue>(1) { this.ReadNestedValue() };
				}
			}
			else
			{
				list = this.ReadNestedResourceSetValues(out uri);
			}
			return new KeyValuePair<ODataNestedResourceInfoWrapper, ODataNestedValues>(new ODataNestedResourceInfoWrapper(this.Read<ODataNestedResourceInfo>(ODataReaderState.NestedResourceInfoEnd)), new ODataNestedValues(list, uri));
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x000B812C File Offset: 0x000B632C
		private ODataNestedValue ReadNestedValue()
		{
			if (this.currentReader.State == ODataReaderState.Primitive)
			{
				Value primitiveValue = this.ReadPrimitive();
				return (TypeValue type, ODataBindingPath bindingPath) => primitiveValue;
			}
			ODataNestedResource? resource = this.ReadResource();
			if (resource != null)
			{
				return (TypeValue type, ODataBindingPath bindingPath) => this.CreateResourceValue(resource.Value, type, bindingPath);
			}
			return (TypeValue type, ODataBindingPath bindingPath) => Value.Null;
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x000B81B8 File Offset: 0x000B63B8
		private List<ODataNestedValue> ReadNestedResourceSetValues(out Uri nextPageUri)
		{
			this.Read(ODataReaderState.ResourceSetStart);
			List<ODataNestedValue> list = new List<ODataNestedValue>();
			while (this.currentReader.State != ODataReaderState.ResourceSetEnd)
			{
				list.Add(this.ReadNestedValue());
			}
			ODataResourceSet odataResourceSet = this.Read<ODataResourceSet>(ODataReaderState.ResourceSetEnd);
			nextPageUri = odataResourceSet.NextPageLink;
			return list;
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x000B81FF File Offset: 0x000B63FF
		private void Read(ODataReaderState expected)
		{
			this.VerifyState(expected);
			this.Read();
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x000B820F File Offset: 0x000B640F
		private T Read<T>(ODataReaderState expected) where T : ODataItem
		{
			this.VerifyState(expected);
			T t = (T)((object)this.currentReader.Item);
			this.Read();
			return t;
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x000B8230 File Offset: 0x000B6430
		private bool Read()
		{
			if (this.currentReader.State == ODataReaderState.Completed)
			{
				return false;
			}
			bool flag;
			try
			{
				flag = this.currentReader.Read();
			}
			catch (Microsoft.Spatial.ParseErrorException ex)
			{
				throw ODataCommonErrors.ODataExceptionMessage(this.environment.Host, ex, this.currentReader.RequestUri, this.environment.Resource.Kind);
			}
			catch (ODataException ex2)
			{
				throw ODataCommonErrors.ODataExceptionMessage(this.environment.Host, ex2, this.currentReader.RequestUri, this.environment.Resource.Kind);
			}
			catch (IOException ex3)
			{
				throw ODataCommonErrors.ODataExceptionMessage(this.environment.Host, ex3, this.currentReader.RequestUri, this.environment.Resource.Kind);
			}
			return flag;
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x000B8314 File Offset: 0x000B6514
		private void VerifyState(ODataReaderState expected)
		{
			if (this.currentReader.State != expected)
			{
				throw ODataCommonErrors.InvalidReaderState(this.currentReader.State.ToString(), this.currentReader.RequestUri);
			}
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x000B835C File Offset: 0x000B655C
		private bool IsNull()
		{
			HttpStatusCode statusCode = (HttpStatusCode)this.currentReader.StatusCode;
			return !this.isResourceSet && statusCode == HttpStatusCode.NoContent;
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x000B8388 File Offset: 0x000B6588
		private void InitializeCurrentReader(ODataReaderWithResponse reader)
		{
			this.currentReader = reader;
			this.omitValues = this.environment.UserSettings.OmitValues;
			if (this.omitValues != null && !reader.IsPreferenceApplied("omit-values", this.omitValues))
			{
				this.omitValues = null;
			}
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x000B83D4 File Offset: 0x000B65D4
		private void DisposeCurrentReader()
		{
			if (this.currentReader != null)
			{
				this.currentReader.Dispose();
				this.currentReader = null;
			}
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x000B83F0 File Offset: 0x000B65F0
		private RecordValue CreateResourceValue(ODataNestedResource resource, TypeValue resourceType, ODataBindingPath bindingPath = null)
		{
			if (resourceType.Equals(TypeValue.Any))
			{
				resourceType = RecordTypeValue.Any.Nullable;
			}
			return this.converter.CreateResourceValue(resource, resourceType.AsRecordType, bindingPath, this.omitValues);
		}

		// Token: 0x04001D7D RID: 7549
		private readonly ODataEnvironment environment;

		// Token: 0x04001D7E RID: 7550
		private readonly GetReader getReader;

		// Token: 0x04001D7F RID: 7551
		private readonly TypeValue itemType;

		// Token: 0x04001D80 RID: 7552
		private readonly bool isResourceSet;

		// Token: 0x04001D81 RID: 7553
		private readonly ODataBindingPath rootBindingPath;

		// Token: 0x04001D82 RID: 7554
		private readonly ODataResourceValueConverter converter;

		// Token: 0x04001D83 RID: 7555
		private ODataReaderWithResponse currentReader;

		// Token: 0x04001D84 RID: 7556
		private string omitValues;

		// Token: 0x04001D85 RID: 7557
		private IValueReference currentValue;

		// Token: 0x04001D86 RID: 7558
		private bool completed;
	}
}
