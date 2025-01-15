using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Tmdl;
using Microsoft.AnalysisServices.Tabular.Tmdl.Schema;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000E7 RID: 231
	public static class TmdlSerializer
	{
		// Token: 0x06000F47 RID: 3911 RVA: 0x00075BC8 File Offset: 0x00073DC8
		public static string SerializeObject(MetadataObject @object, bool qualifyObject = true)
		{
			if (@object == null)
			{
				throw new ArgumentNullException("object");
			}
			NamedMetadataObject namedMetadataObject = @object as NamedMetadataObject;
			if (namedMetadataObject != null && string.IsNullOrEmpty(namedMetadataObject.Name))
			{
				throw new ArgumentException(TomSR.Exception_UnnamedObjectInTmdlObjectSerialization, "object");
			}
			return TmdlSerializer.SerializeObjectImpl(null, @object, qualifyObject);
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00075C14 File Offset: 0x00073E14
		public static string SerializeObject(MetadataObject @object, MetadataSerializationOptions options, bool qualifyObject = true)
		{
			if (@object == null)
			{
				throw new ArgumentNullException("object");
			}
			NamedMetadataObject namedMetadataObject = @object as NamedMetadataObject;
			if (namedMetadataObject != null && string.IsNullOrEmpty(namedMetadataObject.Name))
			{
				throw new ArgumentException(TomSR.Exception_UnnamedObjectInTmdlObjectSerialization, "object");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return TmdlSerializer.SerializeObjectImpl(options, @object, qualifyObject);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00075C6C File Offset: 0x00073E6C
		public static string SerializeDatabase(Database db)
		{
			if (db == null)
			{
				throw new ArgumentNullException("db");
			}
			return TmdlSerializer.SerializeDatabaseImpl(null, db);
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00075C83 File Offset: 0x00073E83
		public static string SerializeDatabase(Database db, MetadataSerializationOptions options)
		{
			if (db == null)
			{
				throw new ArgumentNullException("db");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return TmdlSerializer.SerializeDatabaseImpl(options, db);
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00075CA8 File Offset: 0x00073EA8
		public static void SerializeModelToFolder(Model model, string path)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentNullException("path");
			}
			TmdlSerializer.SerializeModelImpl(null, model, TmdlSerializer.CreateFolderSerializationManager(path, false), null);
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00075CDA File Offset: 0x00073EDA
		public static void SerializeModelToFolder(Model model, string path, MetadataSerializationOptions options)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentNullException("path");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			TmdlSerializer.SerializeModelImpl(options, model, TmdlSerializer.CreateFolderSerializationManager(path, false), null);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00075D1A File Offset: 0x00073F1A
		public static void SerializeModelToCompressedFile(Model model, string path)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentNullException("path");
			}
			TmdlSerializer.SerializeModelImpl(null, model, new ZipMetadataSerializationManager(path, ".tmdl"), null);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x00075D50 File Offset: 0x00073F50
		public static void SerializeModelToCompressedFile(Model model, string path, MetadataSerializationOptions options)
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentNullException("path");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			TmdlSerializer.SerializeModelImpl(options, model, new ZipMetadataSerializationManager(path, ".tmdl"), null);
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00075D9F File Offset: 0x00073F9F
		public static void SerializeDatabaseToFolder(Database db, string path)
		{
			if (db == null)
			{
				throw new ArgumentNullException("db");
			}
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentNullException("path");
			}
			TmdlSerializer.SerializeDatabaseImpl(null, db, TmdlSerializer.CreateFolderSerializationManager(path, true), null);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00075DD1 File Offset: 0x00073FD1
		public static void SerializeDatabaseToFolder(Database db, string path, MetadataSerializationOptions options)
		{
			if (db == null)
			{
				throw new ArgumentNullException("db");
			}
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentNullException("path");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			TmdlSerializer.SerializeDatabaseImpl(options, db, TmdlSerializer.CreateFolderSerializationManager(path, true), null);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00075E11 File Offset: 0x00074011
		public static void SerializeDatabaseToCompressedFile(Database db, string path)
		{
			if (db == null)
			{
				throw new ArgumentNullException("db");
			}
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentNullException("path");
			}
			TmdlSerializer.SerializeDatabaseImpl(null, db, new ZipMetadataSerializationManager(path, ".tmdl"), null);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00075E48 File Offset: 0x00074048
		public static void SerializeDatabaseToCompressedFile(Database db, string path, MetadataSerializationOptions options)
		{
			if (db == null)
			{
				throw new ArgumentNullException("db");
			}
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentNullException("path");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			TmdlSerializer.SerializeDatabaseImpl(options, db, new ZipMetadataSerializationManager(path, ".tmdl"), null);
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00075E97 File Offset: 0x00074097
		public static Model DeserializeModelFromFolder(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			if (!Directory.Exists(path))
			{
				throw new ArgumentException(TomSR.Exception_TmdlSerializerInvalidPath(path), "path");
			}
			return TmdlSerializer.DeserializeModelImpl(null, TmdlContentSource.FileSystem, TmdlSerializer.CreateFolderSerializationManager(path, false), null);
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00075ED4 File Offset: 0x000740D4
		public static Model DeserializeModelFromFolder(string path, MetadataDeserializationOptions options)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			if (!Directory.Exists(path))
			{
				throw new ArgumentException(TomSR.Exception_TmdlSerializerInvalidPath(path), "path");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return TmdlSerializer.DeserializeModelImpl(options, TmdlContentSource.FileSystem, TmdlSerializer.CreateFolderSerializationManager(path, false), null);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00075F2C File Offset: 0x0007412C
		public static Model DeserializeModelFromCompressedFile(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			if (!File.Exists(path))
			{
				throw new ArgumentException(TomSR.Exception_TmdlSerializerInvalidPath(path), "path");
			}
			return TmdlSerializer.DeserializeModelImpl(null, TmdlContentSource.FileSystem, new ZipMetadataSerializationManager(path, ".tmdl"), null);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00075F78 File Offset: 0x00074178
		public static Model DeserializeModelFromCompressedFile(string path, MetadataDeserializationOptions options)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			if (!File.Exists(path))
			{
				throw new ArgumentException(TomSR.Exception_TmdlSerializerInvalidPath(path), "path");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return TmdlSerializer.DeserializeModelImpl(options, TmdlContentSource.FileSystem, new ZipMetadataSerializationManager(path, ".tmdl"), null);
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00075FD2 File Offset: 0x000741D2
		public static Database DeserializeDatabaseFromFolder(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			if (!Directory.Exists(path))
			{
				throw new ArgumentException(TomSR.Exception_TmdlSerializerInvalidPath(path), "path");
			}
			return TmdlSerializer.DeserializeDatabaseImpl(null, TmdlContentSource.FileSystem, TmdlSerializer.CreateFolderSerializationManager(path, true), null);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00076010 File Offset: 0x00074210
		public static Database DeserializeDatabaseFromFolder(string path, MetadataDeserializationOptions options)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			if (!Directory.Exists(path))
			{
				throw new ArgumentException(TomSR.Exception_TmdlSerializerInvalidPath(path), "path");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return TmdlSerializer.DeserializeDatabaseImpl(options, TmdlContentSource.FileSystem, TmdlSerializer.CreateFolderSerializationManager(path, true), null);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00076068 File Offset: 0x00074268
		public static Database DeserializeDatabaseFromCompressedFile(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			if (!File.Exists(path))
			{
				throw new ArgumentException(TomSR.Exception_TmdlSerializerInvalidPath(path), "path");
			}
			return TmdlSerializer.DeserializeDatabaseImpl(null, TmdlContentSource.FileSystem, new ZipMetadataSerializationManager(path, ".tmdl"), null);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x000760B4 File Offset: 0x000742B4
		public static Database DeserializeDatabaseFromCompressedFile(string path, MetadataDeserializationOptions options)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			if (!File.Exists(path))
			{
				throw new ArgumentException(TomSR.Exception_TmdlSerializerInvalidPath(path), "path");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return TmdlSerializer.DeserializeDatabaseImpl(options, TmdlContentSource.FileSystem, new ZipMetadataSerializationManager(path, ".tmdl"), null);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0007610E File Offset: 0x0007430E
		public static string GenerateSchema()
		{
			return TmdlSerializer.GenerateSchemaImpl(null);
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00076116 File Offset: 0x00074316
		public static string GenerateSchema(MetadataSchemaSerializationOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return TmdlSerializer.GenerateSchemaImpl(options);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0007612C File Offset: 0x0007432C
		public static void GenerateSchema(Stream document)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			TmdlSerializer.GenerateSchemaImpl(null, document);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00076143 File Offset: 0x00074343
		public static void GenerateSchema(Stream document, MetadataSchemaSerializationOptions options)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			TmdlSerializer.GenerateSchemaImpl(options, document);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00076168 File Offset: 0x00074368
		internal static MetadataObject DeserializeObject(string tmdl)
		{
			if (string.IsNullOrEmpty(tmdl))
			{
				throw new ArgumentNullException("tmdl");
			}
			TmdlObject tmdlObject = TmdlSerializer.ParseTmdlText(tmdl);
			return TmdlSerializationHelper.DeserializeMetadataObjectFromTmdlObject(typeof(MetadataObject), tmdlObject, null);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x000761A0 File Offset: 0x000743A0
		internal static Model DeserializeModelFromTmdlProject(TmdlProject project)
		{
			if (project == null)
			{
				throw new ArgumentNullException("project");
			}
			TmdlObject tmdlObject;
			TmdlObject tmdlObject2;
			project.Analyze(out tmdlObject, out tmdlObject2);
			return TmdlSerializationHelper.DeserializeMetadataObjectFromTmdlObject<Model>(tmdlObject2, new SerializationActivityContext(MetadataSerializationMode.Tmdl, CompatibilityMode.PowerBI, 1000000, false, true));
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x000761D9 File Offset: 0x000743D9
		internal static Database DeserializeDatabaseFromTmdlProject(TmdlProject project)
		{
			if (project == null)
			{
				throw new ArgumentNullException("project");
			}
			return TmdlSerializationHelper.DeserializeDatabaseFromTmdlProject<Database>(project, null);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x000761F0 File Offset: 0x000743F0
		internal static string SerializeObjectImpl(MetadataSerializationOptions options, MetadataObject @object, bool qualifyObject)
		{
			TmdlSerializationConfiguration tmdlSerializationConfiguration;
			IDisposable disposable;
			ISerializationStrategy serializationStrategy;
			MetadataCompatibilityOptions metadataCompatibilityOptions;
			TmdlSerializationHelper.ProcessSerializationOptions(options, out tmdlSerializationConfiguration, out disposable, out serializationStrategy, out metadataCompatibilityOptions);
			string text;
			try
			{
				CompatibilityMode compatibilityMode;
				int num;
				TmdlSerializationHelper.GetMetadataObjectCompatibilityRestrictions(@object, out compatibilityMode, out num);
				SerializationActivityContext serializationActivityContext = TmdlSerializationHelper.CreateContextBasedOnCompatibilityRestrictionsRequest(metadataCompatibilityOptions, @object, compatibilityMode, num);
				TmdlObject tmdlObject = TmdlSerializationHelper.SerializeMetadataObjectToTmdlObject(tmdlSerializationConfiguration, @object, serializationActivityContext);
				if (qualifyObject && TmdlSerializer.CanBuildQualifiedObjectPath(@object))
				{
					bool flag;
					while (@object.Parent != null && !ObjectTreeHelper.IsChildObject(ObjectType.Model, @object.ObjectType, true, out flag))
					{
						@object = @object.Parent;
						TmdlObject tmdlObject2 = new TmdlObject(@object.ObjectType)
						{
							IsReference = true
						};
						tmdlObject2.Children.Add(tmdlObject);
						NamedMetadataObject namedMetadataObject = @object as NamedMetadataObject;
						if (namedMetadataObject != null)
						{
							tmdlObject2.Name = new ObjectName(new string[] { namedMetadataObject.Name });
						}
						tmdlObject = tmdlObject2;
					}
				}
				StringBuilder stringBuilder = new StringBuilder();
				using (TextWriter textWriter = new StringWriter(stringBuilder))
				{
					using (TmdlTextWriter tmdlTextWriter = new TmdlTextWriter(textWriter, default(TmdlWriterSettings)))
					{
						tmdlObject.WriteTo(tmdlTextWriter);
					}
				}
				text = stringBuilder.ToString();
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return text;
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00076330 File Offset: 0x00074530
		internal static void SerializeModelImpl(MetadataSerializationOptions options, Model model, IMetadataSerializationController controller, object userContext)
		{
			TmdlSerializationConfiguration tmdlSerializationConfiguration;
			IDisposable disposable;
			ISerializationStrategy serializationStrategy;
			MetadataCompatibilityOptions metadataCompatibilityOptions;
			TmdlSerializationHelper.ProcessSerializationOptions(options, out tmdlSerializationConfiguration, out disposable, out serializationStrategy, out metadataCompatibilityOptions);
			try
			{
				CompatibilityMode compatibilityMode;
				int num;
				TmdlSerializationHelper.GetMetadataObjectCompatibilityRestrictions(model, out compatibilityMode, out num);
				SerializationActivityContext serializationActivityContext = TmdlSerializationHelper.CreateContextBasedOnCompatibilityRestrictionsRequest(metadataCompatibilityOptions, model, compatibilityMode, num);
				TmdlProject.Create(TmdlSerializationHelper.SerializeMetadataObjectToTmdlObject(tmdlSerializationConfiguration, model, serializationActivityContext), serializationStrategy, TmdlSerializationOptions.ShouldOrderHintsBeIncludedInTmdlContent(options)).Serialize(controller, userContext);
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0007639C File Offset: 0x0007459C
		internal static string SerializeDatabaseImpl(MetadataSerializationOptions options, Database db)
		{
			TmdlSerializationConfiguration tmdlSerializationConfiguration;
			IDisposable disposable;
			ISerializationStrategy serializationStrategy;
			MetadataCompatibilityOptions metadataCompatibilityOptions;
			TmdlSerializationHelper.ProcessSerializationOptions(options, out tmdlSerializationConfiguration, out disposable, out serializationStrategy, out metadataCompatibilityOptions);
			string text;
			try
			{
				CompatibilityMode compatibilityMode;
				int num;
				TmdlSerializationHelper.GetDatabaseCompatibilityRestrictions(db, db.Model, out compatibilityMode, out num);
				SerializationActivityContext serializationActivityContext = ((db.Model != null) ? TmdlSerializationHelper.CreateContextBasedOnCompatibilityRestrictionsRequest(metadataCompatibilityOptions, db.Model, compatibilityMode, num) : new SerializationActivityContext(MetadataSerializationMode.Tmdl, compatibilityMode, num, false, false));
				TmdlObject tmdlObject = TmdlSerializationHelper.SerializeDatabaseToTmdlObject<Database>(tmdlSerializationConfiguration, db, serializationActivityContext);
				StringBuilder stringBuilder = new StringBuilder();
				using (TextWriter textWriter = new StringWriter(stringBuilder))
				{
					using (TmdlTextWriter tmdlTextWriter = new TmdlTextWriter(textWriter, default(TmdlWriterSettings)))
					{
						tmdlObject.WriteTo(tmdlTextWriter);
					}
				}
				text = stringBuilder.ToString();
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return text;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00076480 File Offset: 0x00074680
		internal static void SerializeDatabaseImpl(MetadataSerializationOptions options, Database db, IMetadataSerializationController controller, object userContext)
		{
			TmdlSerializationConfiguration tmdlSerializationConfiguration;
			IDisposable disposable;
			ISerializationStrategy serializationStrategy;
			MetadataCompatibilityOptions metadataCompatibilityOptions;
			TmdlSerializationHelper.ProcessSerializationOptions(options, out tmdlSerializationConfiguration, out disposable, out serializationStrategy, out metadataCompatibilityOptions);
			try
			{
				CompatibilityMode compatibilityMode;
				int num;
				TmdlSerializationHelper.GetDatabaseCompatibilityRestrictions(db, db.Model, out compatibilityMode, out num);
				SerializationActivityContext serializationActivityContext = ((db.Model != null) ? TmdlSerializationHelper.CreateContextBasedOnCompatibilityRestrictionsRequest(metadataCompatibilityOptions, db.Model, compatibilityMode, num) : new SerializationActivityContext(MetadataSerializationMode.Tmdl, compatibilityMode, num, false, false));
				TmdlProject.Create(TmdlSerializationHelper.SerializeDatabaseToTmdlObject<Database>(tmdlSerializationConfiguration, db, serializationActivityContext), serializationStrategy, TmdlSerializationOptions.ShouldOrderHintsBeIncludedInTmdlContent(options)).Serialize(controller, userContext);
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0007650C File Offset: 0x0007470C
		internal static Model DeserializeModelImpl(MetadataDeserializationOptions options, TmdlContentSource contentSource, IMetadataDeserializationController controller, object context)
		{
			bool flag = true;
			MetadataCompatibilityOptions metadataCompatibilityOptions;
			TmdlSerializationHelper.ProcessDeserializationOptions(options, ref flag, out metadataCompatibilityOptions);
			TmdlObject tmdlObject;
			TmdlObject tmdlObject2;
			TmdlParser.ParseProject(contentSource, MetadataObjectConfiguration.Default.Schema, controller, context).Analyze(out tmdlObject, out tmdlObject2);
			return TmdlSerializationHelper.DeserializeMetadataObjectFromTmdlObject<Model>(tmdlObject2, TmdlSerializationHelper.CreateContextBasedOnDeserializationOptions(flag, metadataCompatibilityOptions));
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x00076550 File Offset: 0x00074750
		internal static Database DeserializeDatabaseImpl(MetadataDeserializationOptions options, TmdlContentSource contentSource, IMetadataDeserializationController controller, object context)
		{
			bool flag = true;
			MetadataCompatibilityOptions metadataCompatibilityOptions;
			TmdlSerializationHelper.ProcessDeserializationOptions(options, ref flag, out metadataCompatibilityOptions);
			return TmdlSerializationHelper.DeserializeDatabaseFromTmdlProject<Database>(TmdlParser.ParseProject(contentSource, MetadataObjectConfiguration.GetFullSchema(), controller, context), TmdlSerializationHelper.CreateContextBasedOnDeserializationOptions(flag, metadataCompatibilityOptions));
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x00076584 File Offset: 0x00074784
		private static string GenerateSchemaImpl(MetadataSchemaSerializationOptions options)
		{
			IDisposable disposable;
			MetadataCompatibilityOptions metadataCompatibilityOptions;
			TmdlSerializationHelper.ProcessSchemaSerializationOptions(options, out disposable, out metadataCompatibilityOptions);
			string text;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				using (TextWriter textWriter = new StringWriter(stringBuilder))
				{
					textWriter.NewLine = MetadataFormattingOptions.GetCurrentEndOfLine();
					using (JsonTextWriter jsonTextWriter = new JsonTextWriter(textWriter))
					{
						TmdlSerializer.ConfigureJsonTextWriter(jsonTextWriter);
						TmdlSchemaHelper.GenerateSchema((metadataCompatibilityOptions == null) ? CompatibilityMode.Unknown : options.Compatibility.CompatibilityMode, (metadataCompatibilityOptions == null) ? (-1) : options.Compatibility.CompatibilityLevel, jsonTextWriter);
					}
				}
				text = stringBuilder.ToString();
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return text;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00076644 File Offset: 0x00074844
		private static void GenerateSchemaImpl(MetadataSchemaSerializationOptions options, Stream document)
		{
			IDisposable disposable;
			MetadataCompatibilityOptions metadataCompatibilityOptions;
			TmdlSerializationHelper.ProcessSchemaSerializationOptions(options, out disposable, out metadataCompatibilityOptions);
			try
			{
				using (TextWriter textWriter = new StreamWriter(document, MetadataFormattingOptions.GetEffectiveEncoding(), 1024, true))
				{
					textWriter.NewLine = MetadataFormattingOptions.GetCurrentEndOfLine();
					using (JsonTextWriter jsonTextWriter = new JsonTextWriter(textWriter))
					{
						TmdlSerializer.ConfigureJsonTextWriter(jsonTextWriter);
						TmdlSchemaHelper.GenerateSchema((metadataCompatibilityOptions == null) ? CompatibilityMode.Unknown : options.Compatibility.CompatibilityMode, (metadataCompatibilityOptions == null) ? (-1) : options.Compatibility.CompatibilityLevel, jsonTextWriter);
					}
				}
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x000766F8 File Offset: 0x000748F8
		private static bool CanBuildQualifiedObjectPath(MetadataObject @object)
		{
			if (@object.Model != null)
			{
				return true;
			}
			MetadataObject metadataObject = @object;
			while (metadataObject.Parent != null)
			{
				metadataObject = metadataObject.Parent;
			}
			bool flag;
			return ObjectTreeHelper.IsChildObject(ObjectType.Model, metadataObject.ObjectType, true, out flag);
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00076734 File Offset: 0x00074934
		private static TmdlSchema BuildAllObjectsTmdlSchema()
		{
			TmdlSchema tmdlSchema = new TmdlSchema();
			foreach (TmdlObjectInfo tmdlObjectInfo in MetadataObjectConfiguration.Default.Schema.GetAllMetadataObjects())
			{
				tmdlSchema.AddRootObject(tmdlObjectInfo);
			}
			tmdlSchema.MakeReadOnly();
			return tmdlSchema;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00076798 File Offset: 0x00074998
		private static TmdlObject ParseTmdlText(string tmdl)
		{
			TmdlDocument tmdlDocument = TmdlParser.ParseDocument(TmdlSerializer.allObjectsTmdlSchema.Value, tmdl);
			TmdlObject tmdlObject = null;
			foreach (TmdlObject tmdlObject2 in tmdlDocument.Objects.Where((TmdlObject o) => o.ObjectType == ObjectType.Model))
			{
				if (tmdlObject == null)
				{
					tmdlObject = tmdlObject2;
				}
				else
				{
					tmdlObject.AddContentOf(tmdlObject2);
				}
			}
			if (tmdlObject != null)
			{
				foreach (TmdlObject tmdlObject3 in tmdlDocument.Objects.Where((TmdlObject o) => o.ObjectType != ObjectType.Model))
				{
					if (tmdlObject3.ObjectType == ObjectType.Database)
					{
						throw new ArgumentException(TomSR.Exception_TmdlSerializerInvalidSingleObjectTmdl_DB, "tmdl");
					}
					tmdlObject.Children.Add(tmdlObject3);
				}
				return tmdlObject;
			}
			if (tmdlDocument.Objects.Count != 1)
			{
				throw new ArgumentException(TomSR.Exception_TmdlSerializerInvalidSingleObjectTmdl_Multi, "tmdl");
			}
			return tmdlDocument.Objects.Single<TmdlObject>();
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x000768D8 File Offset: 0x00074AD8
		private static DirectoryInfo EnsureTargetFolder(string path)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			if (!directoryInfo.Exists)
			{
				directoryInfo.Create();
				directoryInfo.Refresh();
			}
			return directoryInfo;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00076904 File Offset: 0x00074B04
		private static FolderMetadataSerializationManager CreateFolderSerializationManager(string path, bool includeDatabaseFiles)
		{
			FolderMetadataSerializationManager folderMetadataSerializationManager = new FolderMetadataSerializationManager(TmdlSerializer.EnsureTargetFolder(path), ".tmdl", new string[] { ".tmd" });
			if (!includeDatabaseFiles)
			{
				folderMetadataSerializationManager.FilesFilter = TmdlSerializer.DbFilesFilter;
			}
			return folderMetadataSerializationManager;
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00076940 File Offset: 0x00074B40
		private static void ConfigureJsonTextWriter(JsonTextWriter writer)
		{
			IndentationMode indentationMode = MetadataFormattingOptions.Current.IndentationMode;
			if (indentationMode > IndentationMode.Tabs)
			{
				if (indentationMode == IndentationMode.Spaces)
				{
					writer.IndentChar = ' ';
					writer.Indentation = MetadataFormattingOptions.Current.IndentationSize;
				}
			}
			else
			{
				writer.IndentChar = '\t';
				writer.Indentation = 1;
			}
			writer.Formatting = 1;
		}

		// Token: 0x040001CC RID: 460
		private static readonly Func<DirectoryInfo, FileInfo, bool> DbFilesFilter = (DirectoryInfo folder, FileInfo fi) => string.Compare(FolderMetadataSerializationManager.BuildLogicalPath(folder, fi, ".tmdl"), "./database", StringComparison.CurrentCultureIgnoreCase) != 0;

		// Token: 0x040001CD RID: 461
		private static readonly Lazy<TmdlSchema> allObjectsTmdlSchema = new Lazy<TmdlSchema>(new Func<TmdlSchema>(TmdlSerializer.BuildAllObjectsTmdlSchema));
	}
}
