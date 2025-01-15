using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Security.AccessControl;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.AccessControlEntries;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.File
{
	// Token: 0x02000B84 RID: 2948
	internal sealed class FileModule : Module
	{
		// Token: 0x17001947 RID: 6471
		// (get) Token: 0x06005148 RID: 20808 RVA: 0x0011070B File Offset: 0x0010E90B
		public override string Name
		{
			get
			{
				return "File";
			}
		}

		// Token: 0x17001948 RID: 6472
		// (get) Token: 0x06005149 RID: 20809 RVA: 0x00110712 File Offset: 0x0010E912
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(3, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "File.Contents";
						case 1:
							return "Folder.Contents";
						case 2:
							return "Folder.Files";
						default:
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x17001949 RID: 6473
		// (get) Token: 0x0600514A RID: 20810 RVA: 0x0011074D File Offset: 0x0010E94D
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[]
				{
					FileModule.fileResourceKindInfo,
					FileModule.folderResourceKindInfo
				};
			}
		}

		// Token: 0x0600514B RID: 20811 RVA: 0x00110768 File Offset: 0x0010E968
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return new FileModule.FileContentsFunctionValue(hostEnvironment);
				case 1:
					return new FileModule.FolderContentsFunctionValue(hostEnvironment);
				case 2:
					return new FileModule.FolderFilesFunctionValue(hostEnvironment);
				default:
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
			});
		}

		// Token: 0x0600514C RID: 20812 RVA: 0x0011079C File Offset: 0x0010E99C
		public static string LocalPath(string filePath)
		{
			FileHelper.VerifyPath(filePath);
			Uri uri;
			if (Uri.TryCreate(filePath, UriKind.Absolute, out uri) && uri.IsFile)
			{
				return Resource.FileNormalizer.GetNormalizer().GetLocalPath(filePath);
			}
			return filePath;
		}

		// Token: 0x04002C4B RID: 11339
		public const string FileContents = "File.Contents";

		// Token: 0x04002C4C RID: 11340
		public const string FolderContents = "Folder.Contents";

		// Token: 0x04002C4D RID: 11341
		public const string FolderFiles = "Folder.Files";

		// Token: 0x04002C4E RID: 11342
		public const string DataSourceNameString = "File or Folder";

		// Token: 0x04002C4F RID: 11343
		public static readonly OptionRecordDefinition FileOptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("PreserveLastAccessTimes", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, "False")
		});

		// Token: 0x04002C50 RID: 11344
		public static readonly OptionRecordDefinition FolderOptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("PreserveLastAccessTimes", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, "False"),
			new OptionItem("HierarchicalNavigation", NullableTypeValue.Logical, Value.Null, OptionItemOption.ForDsrRoundTripOnly, null, null)
		});

		// Token: 0x04002C51 RID: 11345
		private static readonly ResourceKindInfo fileResourceKindInfo = new FileResourceKindInfo();

		// Token: 0x04002C52 RID: 11346
		private static readonly ResourceKindInfo folderResourceKindInfo = new FolderResourceKindInfo();

		// Token: 0x04002C53 RID: 11347
		private Keys exportKeys;

		// Token: 0x02000B85 RID: 2949
		private enum Exports
		{
			// Token: 0x04002C55 RID: 11349
			FileContents,
			// Token: 0x04002C56 RID: 11350
			FolderContents,
			// Token: 0x04002C57 RID: 11351
			FolderFiles,
			// Token: 0x04002C58 RID: 11352
			Count
		}

		// Token: 0x02000B86 RID: 2950
		private static class FileContentsBinaryValue
		{
			// Token: 0x0600514F RID: 20815 RVA: 0x00110868 File Offset: 0x0010EA68
			public static BinaryValue New(IEngineHost host, string filePath, OptionsRecord optionsRecord, Func<IDisposable> impersonate)
			{
				IHostProgress hostProgress = ProgressService.GetHostProgress(host, "File", Path.GetFileName(filePath));
				bool @bool = optionsRecord.GetBool("PreserveLastAccessTimes", false);
				FileStreamBinaryValue fileStreamBinaryValue = new FileStreamBinaryValue(host, filePath, hostProgress, impersonate, @bool);
				return fileStreamBinaryValue.NewMeta(DataSource.CreateLocalDataSourceRecordValue(filePath, impersonate)).AsBinary.WithExpressionFromValue(fileStreamBinaryValue);
			}
		}

		// Token: 0x02000B87 RID: 2951
		public sealed class FileContentsFunctionValue : NativeFunctionValue2<BinaryValue, TextValue, Value>
		{
			// Token: 0x06005150 RID: 20816 RVA: 0x001108B7 File Offset: 0x0010EAB7
			public FileContentsFunctionValue(IEngineHost host)
				: base(TypeValue.Binary, 1, "path", TypeValue.Text, "options", FileModule.FileContentsFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x1700194A RID: 6474
			// (get) Token: 0x06005151 RID: 20817 RVA: 0x001108E0 File Offset: 0x0010EAE0
			public override IFunctionIdentity FunctionIdentity
			{
				get
				{
					return new FunctionTypeIdentity(typeof(FileModule.FileContentsFunctionValue));
				}
			}

			// Token: 0x06005152 RID: 20818 RVA: 0x001108F4 File Offset: 0x0010EAF4
			public override BinaryValue TypedInvoke(TextValue path, Value options)
			{
				BinaryValue binaryValue;
				using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/IO/File/FileContent", TraceEventType.Information, null))
				{
					IResource resource = FileModule.FileContentsFunctionValue.GetResource(path);
					hostTrace.AddResource(resource);
					Func<IDisposable> func = FileHelper.VerifyPermissionAndGetImpersonationWrapper(this.host, resource);
					string nonNormalizedPath = resource.NonNormalizedPath;
					OptionsRecord optionsRecord = FileModule.FileOptionRecord.CreateOptions("File.Contents", options);
					try
					{
						if (Directory.Exists(nonNormalizedPath))
						{
							throw FileErrors.FilePathExpected(nonNormalizedPath);
						}
						binaryValue = FileModule.FileContentsBinaryValue.New(this.host, nonNormalizedPath, optionsRecord, func);
					}
					catch (SecurityException ex)
					{
						hostTrace.Add(ex, true);
						throw DataSourceException.NewAccessAuthorizationError(this.host, resource, nonNormalizedPath, null, null);
					}
					catch (Exception ex2)
					{
						hostTrace.Add(ex2, true);
						throw FileErrors.HandleException(ex2, path);
					}
				}
				return binaryValue;
			}

			// Token: 0x1700194B RID: 6475
			// (get) Token: 0x06005153 RID: 20819 RVA: 0x0011070B File Offset: 0x0010E90B
			public override string PrimaryResourceKind
			{
				get
				{
					return "File";
				}
			}

			// Token: 0x06005154 RID: 20820 RVA: 0x001109D0 File Offset: 0x0010EBD0
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Dictionary<string, IExpression> dictionary;
				Value value;
				if (FileModule.FileContentsFunctionValue.pattern.TryMatch(expression, out dictionary) && dictionary.TryGetConstant("path", out value))
				{
					IResource resource = Resource.New("File", value.AsString);
					if (FileDataSourceLocation.Factory.TryCreateFromResource(resource, false, out location))
					{
						foundOptions = RecordValue.Empty;
						unknownOptions = Keys.Empty;
						return true;
					}
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x06005155 RID: 20821 RVA: 0x00110A37 File Offset: 0x0010EC37
			private static IResource GetResource(TextValue path)
			{
				return Resource.New("File", FileModule.LocalPath(path.AsString));
			}

			// Token: 0x04002C59 RID: 11353
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__path, _o_options)" });

			// Token: 0x04002C5A RID: 11354
			private static readonly TypeValue optionsType = FileModule.FileOptionRecord.CreateRecordType().Nullable;

			// Token: 0x04002C5B RID: 11355
			private readonly IEngineHost host;
		}

		// Token: 0x02000B88 RID: 2952
		private sealed class FolderContentsFunctionValue : NativeFunctionValue2<TableValue, TextValue, Value>
		{
			// Token: 0x06005157 RID: 20823 RVA: 0x00110A7C File Offset: 0x0010EC7C
			public FolderContentsFunctionValue(IEngineHost host)
				: base(FileHelper.FolderResultTypeValue(FileHelper.FolderOptions.EnumerateFoldersAndFiles), 1, "path", TypeValue.Text, "options", FileModule.FolderContentsFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x1700194C RID: 6476
			// (get) Token: 0x06005158 RID: 20824 RVA: 0x00110AA6 File Offset: 0x0010ECA6
			public override IFunctionIdentity FunctionIdentity
			{
				get
				{
					return new FunctionTypeIdentity(typeof(FileModule.FolderContentsFunctionValue));
				}
			}

			// Token: 0x06005159 RID: 20825 RVA: 0x00110AB8 File Offset: 0x0010ECB8
			public override TableValue TypedInvoke(TextValue path, Value options)
			{
				IResource resource = FileModule.FolderContentsFunctionValue.GetResource(path);
				Func<IDisposable> func = FileHelper.VerifyPermissionAndGetImpersonationWrapper(this.host, resource);
				string nonNormalizedPath = resource.NonNormalizedPath;
				OptionsRecord optionsRecord = FileModule.FolderOptionRecord.CreateOptions("Folder.Contents", options);
				return FileModule.FolderContentsTableValue.New(this.host, nonNormalizedPath, optionsRecord, func, "*", null, FileHelper.FolderOptions.EnumerateFoldersAndFiles);
			}

			// Token: 0x1700194D RID: 6477
			// (get) Token: 0x0600515A RID: 20826 RVA: 0x00110B06 File Offset: 0x0010ED06
			public override string PrimaryResourceKind
			{
				get
				{
					return "Folder";
				}
			}

			// Token: 0x0600515B RID: 20827 RVA: 0x00110B10 File Offset: 0x0010ED10
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				location = null;
				foundOptions = RecordValue.Empty;
				unknownOptions = Keys.Empty;
				Dictionary<string, IExpression> dictionary;
				string text;
				if (FileModule.FolderContentsFunctionValue.pattern.TryMatch(expression, out dictionary) && dictionary.TryGetStringConstant("path", out text))
				{
					RecordValue recordValue = RecordValue.New(Keys.New("HierarchicalNavigation"), new Value[] { LogicalValue.False });
					string text2 = FileModule.LocalPath(text);
					string text3;
					if (dictionary.TryGetStringConstant("name", out text3))
					{
						Value value;
						if (dictionary.TryGetConstant("isFile", out value) && value.IsLogical)
						{
							if (value.AsBoolean)
							{
								location = new FileDataSourceLocation();
								recordValue = RecordValue.Empty;
							}
							else
							{
								location = new FolderDataSourceLocation();
							}
							location.Address["path"] = Path.Combine(text2, text3);
						}
						if (location == null)
						{
							return false;
						}
					}
					else
					{
						location = new FolderDataSourceLocation();
						location.Address["path"] = text2;
					}
					IExpression expression2;
					if (dictionary.TryGetValue("options", out expression2))
					{
						RecordValue record = ExpressionAnalysis.GetRecord(expression2);
						if (record == null)
						{
							foundOptions = null;
							unknownOptions = null;
						}
						else
						{
							foundOptions = ExpressionAnalysis.RemovePlaceholders(record, out unknownOptions).Concatenate(recordValue).AsRecord;
						}
					}
					else
					{
						foundOptions = recordValue;
					}
					return true;
				}
				location = null;
				return false;
			}

			// Token: 0x0600515C RID: 20828 RVA: 0x00110C3C File Offset: 0x0010EE3C
			private static IResource GetResource(TextValue path)
			{
				return Resource.New("Folder", FileModule.LocalPath(path.AsString));
			}

			// Token: 0x04002C5C RID: 11356
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__path, _o_options)", "__func(__path, _o_options){[Name = __name] meta [Is File = __isFile]}[Content]" });

			// Token: 0x04002C5D RID: 11357
			private static readonly TypeValue optionsType = FileModule.FolderOptionRecord.CreateRecordType().Nullable;

			// Token: 0x04002C5E RID: 11358
			private readonly IEngineHost host;
		}

		// Token: 0x02000B89 RID: 2953
		private static class FolderContentsRecordValue
		{
			// Token: 0x0600515E RID: 20830 RVA: 0x00110C8C File Offset: 0x0010EE8C
			public static RecordValue New(IEngineHost host, FileHelper.AccessControlEntriesCache aceCache, FileHelper.FileData fileData, OptionsRecord optionsRecord, Func<IDisposable> impersonate)
			{
				Func<Value> func = () => FileHelper.GetChangeTime(fileData.FullName, impersonate);
				Value value = LogicalValue.New(FileHelper.IsSymbolicLink(fileData));
				Value value2 = LogicalValue.New(FileHelper.IsMountPoint(fileData));
				Value content;
				TextValue textValue;
				Func<Value> func2;
				Value value3;
				Func<TextValue> func3;
				if (fileData.IsFolder)
				{
					content = FileModule.FolderContentsTableValue.New(host, fileData.FullName, optionsRecord, impersonate);
					textValue = TextValue.Empty;
					func2 = FileModule.FolderContentsRecordValue.delayedFolderKind;
					value3 = Value.Null;
					func3 = () => TextValue.Empty;
				}
				else
				{
					content = FileModule.FileContentsBinaryValue.New(host, fileData.FullName, optionsRecord, impersonate);
					try
					{
						textValue = FileHelper.GetFileExtension(fileData.FullName);
						func3 = () => content.MetaValue["Content.Type"].AsText;
						func2 = () => FileHelper.GetFileKind(content.MetaValue["Content.Type"].AsString);
					}
					catch (ValueException)
					{
						textValue = TextValue.Empty;
						func2 = FileModule.FolderContentsRecordValue.delayedFileKind;
						func3 = () => TextValue.Empty;
					}
					value3 = NumberValue.New(fileData.Length);
				}
				RecordValue recordValue = RecordValue.New(AccessControlEntriesModule.AccessControlEntry.TableMetadataKeys, (int index) => FileHelper.GetAccessControlEntries(aceCache, fileData.FullName, fileData.IsFolder, FileSystemRights.ReadAttributes, impersonate));
				RecordValue recordValue2 = RecordValue.New(AccessControlEntriesModule.AccessControlEntry.TableMetadataKeys, (int index) => FileHelper.GetAccessControlEntries(aceCache, fileData.FullName, fileData.IsFolder, FileSystemRights.ReadData, impersonate));
				content = content.NewMeta(content.MetaValue.Concatenate(recordValue2).AsRecord);
				return RecordValue.New(FileHelper.FileEntryKeys, new Value[]
				{
					content,
					TextValue.New(fileData.Name),
					textValue,
					DateTimeValue.New(fileData.LastAccessTime).NewMeta(recordValue),
					DateTimeValue.New(fileData.LastWriteTime).NewMeta(recordValue),
					DateTimeValue.New(fileData.CreationTime).NewMeta(recordValue),
					FileHelper.CreateFileAttributesRecordValue(fileData.Attributes, func3, func2, value3, func, value, value2).NewMeta(recordValue),
					TextValue.New(fileData.FolderPath.TrimEnd(new char[] { Path.DirectorySeparatorChar }) + Path.DirectorySeparatorChar.ToString())
				});
			}

			// Token: 0x04002C5F RID: 11359
			private static readonly Func<Value> delayedFolderKind = () => FileHelper.FolderKind;

			// Token: 0x04002C60 RID: 11360
			private static readonly Func<Value> delayedFileKind = () => FileHelper.FileKind;
		}

		// Token: 0x02000B8C RID: 2956
		private sealed class FolderContentsTableValue : TableValue, IOptimizedValue
		{
			// Token: 0x0600516C RID: 20844 RVA: 0x00111008 File Offset: 0x0010F208
			private FolderContentsTableValue(IEngineHost host, string folderPath, OptionsRecord optionsRecord, Func<IDisposable> impersonate, string fileNamePattern, FunctionValue localFilter, FileHelper.FolderOptions folderOptions)
			{
				this.host = host;
				this.folderPath = folderPath;
				this.optionsRecord = optionsRecord;
				this.impersonate = impersonate;
				this.fileNamePattern = fileNamePattern;
				this.folderOptions = folderOptions;
				this.localFilter = localFilter;
			}

			// Token: 0x0600516D RID: 20845 RVA: 0x00111045 File Offset: 0x0010F245
			public static FileModule.FolderContentsTableValue New(IEngineHost host, string folderPath, OptionsRecord optionsRecord, Func<IDisposable> impersonate)
			{
				return FileModule.FolderContentsTableValue.New(host, folderPath, optionsRecord, impersonate, "*", null, FileHelper.FolderOptions.EnumerateFoldersAndFiles);
			}

			// Token: 0x0600516E RID: 20846 RVA: 0x00111057 File Offset: 0x0010F257
			public static FileModule.FolderContentsTableValue New(IEngineHost host, string folderPath, OptionsRecord optionsRecord, Func<IDisposable> impersonate, string fileNamePattern, FunctionValue localFilter, FileHelper.FolderOptions folderOptions)
			{
				return new FileModule.FolderContentsTableValue(host, folderPath, optionsRecord, impersonate, fileNamePattern, localFilter, folderOptions);
			}

			// Token: 0x1700194E RID: 6478
			// (get) Token: 0x0600516F RID: 20847 RVA: 0x00111068 File Offset: 0x0010F268
			public override TypeValue Type
			{
				get
				{
					return FileSystemTableHelper.AddTypeMetadata(FileHelper.FolderResultTypeValue(this.folderOptions), this.folderPath, Path.DirectorySeparatorChar.ToString(), !FileHelper.EnumerateDeep(this.folderOptions));
				}
			}

			// Token: 0x1700194F RID: 6479
			// (get) Token: 0x06005170 RID: 20848 RVA: 0x001110A6 File Offset: 0x0010F2A6
			public override Query Query
			{
				get
				{
					return new FileModule.FolderContentsTableValue.FolderContentsQuery(this);
				}
			}

			// Token: 0x17001950 RID: 6480
			// (get) Token: 0x06005171 RID: 20849 RVA: 0x001110B0 File Offset: 0x0010F2B0
			public override IExpression Expression
			{
				get
				{
					FileHelper.FolderOptions folderOptions = this.folderOptions;
					FunctionValue functionValue;
					if (folderOptions - FileHelper.FolderOptions.EnumerateFiles > 1)
					{
						if (folderOptions != FileHelper.FolderOptions.EnumerateFoldersAndFiles)
						{
							return base.Expression;
						}
						functionValue = new FileModule.FolderContentsFunctionValue(this.host);
					}
					else
					{
						functionValue = new FileModule.FolderFilesFunctionValue(this.host);
					}
					IExpression expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(functionValue), new ConstantExpressionSyntaxNode(TextValue.New(this.folderPath)), new ConstantExpressionSyntaxNode(this.optionsRecord.AsRecord));
					if (this.localFilter != null)
					{
						expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.SelectRows), expression, new ConstantExpressionSyntaxNode(this.localFilter));
					}
					return expression;
				}
			}

			// Token: 0x06005172 RID: 20850 RVA: 0x00111142 File Offset: 0x0010F342
			public void VerifyActionPermitted()
			{
				this.host.VerifyActionPermitted(Resource.New("Folder", this.folderPath));
			}

			// Token: 0x06005173 RID: 20851 RVA: 0x00111160 File Offset: 0x0010F360
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return new FileModule.FolderContentsTableValue.FolderContentsEnumerator(this.host, this.folderPath, this.optionsRecord, this.impersonate, this.localFilter, FileHelper.EnumerateFolderContents(this.folderPath, this.fileNamePattern, this.folderOptions, this.impersonate, this.host.QueryService<ITimeZoneService>().DefaultTimeZone));
			}

			// Token: 0x06005174 RID: 20852 RVA: 0x001111C0 File Offset: 0x0010F3C0
			public override TableValue SelectRows(FunctionValue condition)
			{
				FileModule.FolderContentsTableValue folderContentsTableValue;
				if (this.TrySelectRows(condition, out folderContentsTableValue))
				{
					return folderContentsTableValue;
				}
				return base.SelectRows(condition);
			}

			// Token: 0x06005175 RID: 20853 RVA: 0x001111E4 File Offset: 0x0010F3E4
			private bool TryGetColumnText(string columnName, QueryExpression query1, QueryExpression query2, out string text)
			{
				ColumnAccessQueryExpression columnAccessQueryExpression = query1 as ColumnAccessQueryExpression;
				if (columnAccessQueryExpression != null && this.Columns[columnAccessQueryExpression.Column] == columnName)
				{
					ConstantQueryExpression constantQueryExpression = query2 as ConstantQueryExpression;
					if (constantQueryExpression != null && constantQueryExpression.Value.IsText)
					{
						text = constantQueryExpression.Value.AsString;
						return true;
					}
				}
				text = null;
				return false;
			}

			// Token: 0x06005176 RID: 20854 RVA: 0x00111240 File Offset: 0x0010F440
			private bool TrySelectRows(FunctionValue condition, out FileModule.FolderContentsTableValue tableValue)
			{
				QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(this.Type.AsTableType.ItemType, condition);
				return this.TryApply(condition, queryExpression, out tableValue);
			}

			// Token: 0x06005177 RID: 20855 RVA: 0x00111270 File Offset: 0x0010F470
			private bool TryApply(FunctionValue condition, QueryExpression query, out FileModule.FolderContentsTableValue tableValue)
			{
				QueryExpressionKind kind = query.Kind;
				if (kind == QueryExpressionKind.Binary)
				{
					return this.TryApplyBinary(condition, (BinaryQueryExpression)query, out tableValue);
				}
				if (kind != QueryExpressionKind.Invocation)
				{
					tableValue = null;
					return false;
				}
				return this.TryApplyInvocation(condition, (InvocationQueryExpression)query, out tableValue);
			}

			// Token: 0x06005178 RID: 20856 RVA: 0x001112B0 File Offset: 0x0010F4B0
			private bool TryApplyBinary(FunctionValue condition, BinaryQueryExpression binary, out FileModule.FolderContentsTableValue tableValue)
			{
				BinaryOperator2 @operator = binary.Operator;
				if (@operator == BinaryOperator2.Equals)
				{
					return this.TryApplyEquals(condition, binary, out tableValue);
				}
				if (@operator == BinaryOperator2.And)
				{
					return this.TryApplyAnd(condition, binary, out tableValue);
				}
				tableValue = null;
				return false;
			}

			// Token: 0x06005179 RID: 20857 RVA: 0x001112E5 File Offset: 0x0010F4E5
			private bool TryApplyAnd(FunctionValue condition, BinaryQueryExpression binary, out FileModule.FolderContentsTableValue tableValue)
			{
				return this.TryApply(condition, binary.Left, out tableValue) && tableValue.TryApply(condition, binary.Right, out tableValue);
			}

			// Token: 0x0600517A RID: 20858 RVA: 0x00111308 File Offset: 0x0010F508
			private bool TryApplyEquals(FunctionValue condition, BinaryQueryExpression binary, out FileModule.FolderContentsTableValue tableValue)
			{
				string text;
				if ((this.TryGetColumnText("Extension", binary.Left, binary.Right, out text) || this.TryGetColumnText("Extension", binary.Right, binary.Left, out text)) && FileHelper.TryApplyExtension(this.fileNamePattern, text, out text))
				{
					tableValue = FileModule.FolderContentsTableValue.New(this.host, this.folderPath, this.optionsRecord, this.impersonate, text, FileModule.FolderContentsTableValue.CombineFilters(this.localFilter, condition), this.folderOptions);
					return true;
				}
				if ((this.TryGetColumnText("Name", binary.Left, binary.Right, out text) || this.TryGetColumnText("Name", binary.Right, binary.Left, out text)) && FileHelper.TryApplyFileName(this.fileNamePattern, text, out text))
				{
					tableValue = FileModule.FolderContentsTableValue.New(this.host, this.folderPath, this.optionsRecord, this.impersonate, text, FileModule.FolderContentsTableValue.CombineFilters(this.localFilter, condition), this.folderOptions);
					return true;
				}
				if ((this.TryGetColumnText("Folder Path", binary.Left, binary.Right, out text) || this.TryGetColumnText("Folder Path", binary.Right, binary.Left, out text)) && text.StartsWith(this.folderPath, StringComparison.OrdinalIgnoreCase) && FileHelper.EndsWithDirectorySeparatorChar(text))
				{
					FileHelper.FolderOptions folderOptions = this.folderOptions & ~FileHelper.FolderOptions.EnumerateDeep;
					string text2 = FileHelper.GetDirectoryName(text) ?? text;
					tableValue = FileModule.FolderContentsTableValue.New(this.host, text2, this.optionsRecord, this.impersonate, this.fileNamePattern, FileModule.FolderContentsTableValue.CombineFilters(this.localFilter, condition), folderOptions);
					return true;
				}
				tableValue = null;
				return false;
			}

			// Token: 0x0600517B RID: 20859 RVA: 0x001114A0 File Offset: 0x0010F6A0
			private bool TryApplyInvocation(FunctionValue condition, InvocationQueryExpression invocation, out FileModule.FolderContentsTableValue tableValue)
			{
				ConstantQueryExpression constantQueryExpression = invocation.Function as ConstantQueryExpression;
				if (constantQueryExpression != null && (constantQueryExpression.Value.Equals(Library.Text.Contains) || constantQueryExpression.Value.Equals(Library.Text.EndsWith) || constantQueryExpression.Value.Equals(Library.Text.StartsWith)))
				{
					string directoryName;
					if (this.TryGetColumnText("Name", invocation.Arguments[0], invocation.Arguments[1], out directoryName))
					{
						if (FileHelper.TryCombinePattern(this.fileNamePattern, constantQueryExpression.Value, directoryName, out directoryName) && FileHelper.TryApplyFileName(this.fileNamePattern, directoryName, out directoryName))
						{
							tableValue = FileModule.FolderContentsTableValue.New(this.host, this.folderPath, this.optionsRecord, this.impersonate, directoryName, FileModule.FolderContentsTableValue.CombineFilters(this.localFilter, condition), this.folderOptions);
							return true;
						}
					}
					else if (this.TryGetColumnText("Extension", invocation.Arguments[0], invocation.Arguments[1], out directoryName))
					{
						if (FileHelper.TryCombinePattern(FileHelper.RemoveExtensionSeparator(Path.GetExtension(this.fileNamePattern)), constantQueryExpression.Value, FileHelper.RemoveExtensionSeparator(directoryName), out directoryName) && FileHelper.TryApplyExtension(this.fileNamePattern, FileHelper.AddExtensionSeparator(directoryName), out directoryName))
						{
							tableValue = FileModule.FolderContentsTableValue.New(this.host, this.folderPath, this.optionsRecord, this.impersonate, directoryName, FileModule.FolderContentsTableValue.CombineFilters(this.localFilter, condition), this.folderOptions);
							return true;
						}
					}
					else if (constantQueryExpression.Value.Equals(Library.Text.StartsWith) && this.TryGetColumnText("Folder Path", invocation.Arguments[0], invocation.Arguments[1], out directoryName) && directoryName.StartsWith(this.folderPath, StringComparison.OrdinalIgnoreCase))
					{
						if (directoryName.TrimEnd(new char[] { Path.DirectorySeparatorChar }).Length == this.folderPath.TrimEnd(new char[] { Path.DirectorySeparatorChar }).Length)
						{
							tableValue = this;
							return true;
						}
						if (FileHelper.EnumerateDeep(this.folderOptions))
						{
							directoryName = FileHelper.GetDirectoryName(directoryName);
							if (directoryName.Length > this.folderPath.Length)
							{
								tableValue = FileModule.FolderContentsTableValue.New(this.host, directoryName, this.optionsRecord, this.impersonate, this.fileNamePattern, FileModule.FolderContentsTableValue.CombineFilters(this.localFilter, condition), this.folderOptions);
								return true;
							}
						}
					}
				}
				tableValue = null;
				return false;
			}

			// Token: 0x0600517C RID: 20860 RVA: 0x00111708 File Offset: 0x0010F908
			private static FunctionValue CombineFilters(FunctionValue filter1, FunctionValue filter2)
			{
				if (filter1 == null)
				{
					return filter2;
				}
				if (filter2 == null)
				{
					return filter1;
				}
				RecordTypeValue itemType = FileHelper.FolderResultTypeValue(FileHelper.FolderOptions.EnumerateFoldersAndFiles).ItemType;
				QueryExpression queryExpression;
				QueryExpression queryExpression2;
				if (QueryExpressionBuilder.TryToQueryExpression(itemType, filter1, out queryExpression) && QueryExpressionBuilder.TryToQueryExpression(itemType, filter2, out queryExpression2))
				{
					return QueryExpressionAssembler.Assemble(itemType.FieldKeys, new BinaryQueryExpression(BinaryOperator2.And, queryExpression, queryExpression2));
				}
				return new FileModule.FolderContentsTableValue.CombinedFilterFunctionValue(filter1, filter2);
			}

			// Token: 0x04002C68 RID: 11368
			private readonly IEngineHost host;

			// Token: 0x04002C69 RID: 11369
			private readonly FileHelper.FolderOptions folderOptions;

			// Token: 0x04002C6A RID: 11370
			private readonly string folderPath;

			// Token: 0x04002C6B RID: 11371
			private readonly OptionsRecord optionsRecord;

			// Token: 0x04002C6C RID: 11372
			private readonly Func<IDisposable> impersonate;

			// Token: 0x04002C6D RID: 11373
			private readonly string fileNamePattern;

			// Token: 0x04002C6E RID: 11374
			private readonly FunctionValue localFilter;

			// Token: 0x02000B8D RID: 2957
			private class CombinedFilterFunctionValue : NativeFunctionValue1<LogicalValue, Value>
			{
				// Token: 0x0600517D RID: 20861 RVA: 0x0011175D File Offset: 0x0010F95D
				public CombinedFilterFunctionValue(FunctionValue filter1, FunctionValue filter2)
					: base(TypeValue.Logical, "row", TypeValue.Any)
				{
					this.filter1 = filter1;
					this.filter2 = filter2;
				}

				// Token: 0x0600517E RID: 20862 RVA: 0x00111782 File Offset: 0x0010F982
				public override LogicalValue TypedInvoke(Value row)
				{
					return LogicalValue.New(this.filter1.Invoke(row).AsBoolean && this.filter2.Invoke(row).AsBoolean);
				}

				// Token: 0x04002C6F RID: 11375
				private readonly FunctionValue filter1;

				// Token: 0x04002C70 RID: 11376
				private readonly FunctionValue filter2;
			}

			// Token: 0x02000B8E RID: 2958
			private sealed class FolderContentsQuery : FilteredTableQuery
			{
				// Token: 0x0600517F RID: 20863 RVA: 0x001117B0 File Offset: 0x0010F9B0
				public FolderContentsQuery(FileModule.FolderContentsTableValue table)
					: base(table, table.host)
				{
				}

				// Token: 0x17001951 RID: 6481
				// (get) Token: 0x06005180 RID: 20864 RVA: 0x001117BF File Offset: 0x0010F9BF
				private FileModule.FolderContentsTableValue FolderContentsTable
				{
					get
					{
						return (FileModule.FolderContentsTableValue)base.Table;
					}
				}

				// Token: 0x06005181 RID: 20865 RVA: 0x001117CC File Offset: 0x0010F9CC
				public override ActionValue InsertRows(Query rowsToInsert)
				{
					this.FolderContentsTable.VerifyActionPermitted();
					return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.InsertRows(rowsToInsert, countOnlyTable));
				}

				// Token: 0x06005182 RID: 20866 RVA: 0x001117FC File Offset: 0x0010F9FC
				private ActionValue InsertRows(Query rowsToInsert, bool countOnlyTable)
				{
					if (!countOnlyTable)
					{
						throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, new QueryTableValue(this), null);
					}
					long num = 0L;
					List<IValueReference> list = new List<IValueReference>();
					foreach (IValueReference valueReference in rowsToInsert.GetRows())
					{
						num += 1L;
						RecordValue asRecord = valueReference.Value.AsRecord;
						string folder = FileModule.FolderContentsTableValue.FolderContentsQuery.FieldOrDefault(asRecord, "Folder Path", this.FolderContentsTable.folderPath);
						if (!FileHelper.CanNormalizePath(folder))
						{
							throw ValueException.NewExpressionError<Message0>(Strings.Resource_FolderPath_Absolute, asRecord, null);
						}
						string asString = asRecord["Name"].AsString;
						if (FileHelper.ContainsPathSeparator(asString))
						{
							throw ValueException.NewExpressionError<Message0>(Strings.File_NoPathInName, asRecord, null);
						}
						string fullPath = FileHelper.GetFullPath(folder, asString);
						if (!FileHelper.IsSubPath(this.FolderContentsTable.folderPath, fullPath))
						{
							throw ValueException.NewExpressionError<Message1>(Strings.File_PathMustBeSubPath(this.FolderContentsTable.folderPath), asRecord, null);
						}
						if (FileModule.FolderContentsTableValue.FolderContentsQuery.FieldOrDefault(asRecord, "Extension", FileHelper.GetFileExtension(asString).AsString) != FileHelper.GetFileExtension(asString).AsString)
						{
							throw ValueException.NewExpressionError<Message0>(Strings.File_ExtensionMustMatchName, asRecord, null);
						}
						if (asRecord.Keys.Contains("Date accessed") || asRecord.Keys.Contains("Date modified") || asRecord.Keys.Contains("Date created"))
						{
							throw ValueException.NewExpressionError<Message0>(Strings.File_SetDatesNotSupported, asRecord, null);
						}
						Value value;
						if (asRecord.TryGetValue("Attributes", out value))
						{
							using (Keys.StringKeysEnumerator enumerator2 = value.AsRecord.Keys.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									if (enumerator2.Current != "Directory")
									{
										throw ValueException.NewExpressionError<Message0>(Strings.File_SetAttributesNotSupported, asRecord, null);
									}
								}
							}
						}
						if (FileModule.FolderContentsTableValue.FolderContentsQuery.IsFolder(asRecord))
						{
							TableValue tableValue = FileModule.FolderContentsTableValue.FolderContentsQuery.EmptyContentTable;
							Value value2;
							if (asRecord.TryGetValue("Content", out value2) && !value2.IsNull)
							{
								tableValue = value2.AsTable;
							}
							list.Add(ActionValue.New(delegate
							{
								using (this.FolderContentsTable.impersonate())
								{
									FileHelper.CreateDirectory(fullPath);
								}
								return Value.Null;
							}));
							ActionValue actionValue = FileModule.FolderContentsTableValue.New(this.FolderContentsTable.host, fullPath, this.FolderContentsTable.optionsRecord, this.FolderContentsTable.impersonate).InsertRows(tableValue);
							list.Add(actionValue);
						}
						else
						{
							BinaryValue binaryValue = BinaryValue.Empty;
							Value value3;
							if (asRecord.TryGetValue("Content", out value3) && !value3.IsNull)
							{
								binaryValue = value3.AsBinary;
							}
							list.Add(ActionValue.New(delegate
							{
								using (this.FolderContentsTable.impersonate())
								{
									if (FileHelper.FileExists(fullPath))
									{
										throw ValueException.NewDataSourceError<Message1>(Strings.File_FileExists(fullPath), new QueryTableValue(this), null);
									}
								}
								return Value.Null;
							}));
							list.Add(ActionValue.New(delegate
							{
								using (this.FolderContentsTable.impersonate())
								{
									FileHelper.CreateDirectory(folder);
								}
								return Value.Null;
							}));
							ActionValue actionValue2 = FileModule.FileContentsBinaryValue.New(this.FolderContentsTable.host, fullPath, this.FolderContentsTable.optionsRecord, this.FolderContentsTable.impersonate).Replace(binaryValue);
							list.Add(actionValue2);
						}
					}
					list.Add(ActionModule.Action.Return.Invoke(NumberValue.New(num)));
					list.Add(new ReturnTypedTableFromCountFunctionValue(new QueryTableValue(this).Type.AsTableType));
					return ActionValue.New(ListValue.New(list)).ClearCache(this.FolderContentsTable.host);
				}

				// Token: 0x06005183 RID: 20867 RVA: 0x00111B9C File Offset: 0x0010FD9C
				public override ActionValue UpdateRows(ColumnUpdates columnUpdates, FunctionValue selector)
				{
					this.FolderContentsTable.VerifyActionPermitted();
					return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.UpdateRows(columnUpdates, selector, countOnlyTable));
				}

				// Token: 0x06005184 RID: 20868 RVA: 0x00111BD4 File Offset: 0x0010FDD4
				private ActionValue UpdateRows(ColumnUpdates columnUpdates, FunctionValue selector, bool countOnlyTable)
				{
					if (!countOnlyTable)
					{
						throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, new QueryTableValue(this), null);
					}
					long num = 0L;
					List<IValueReference> list = new List<IValueReference>();
					foreach (IValueReference valueReference in base.Table.SelectRows(selector))
					{
						num += 1L;
						RecordValue asRecord = valueReference.Value.AsRecord;
						string asString = asRecord["Name"].AsString;
						string text = asString;
						string text2 = FileModule.FolderContentsTableValue.FolderContentsQuery.FieldOrDefault(asRecord, "Folder Path", this.FolderContentsTable.folderPath);
						string text3 = text2;
						foreach (KeyValuePair<int, FunctionValue> keyValuePair in columnUpdates.Updates)
						{
							string asString2 = asRecord.KeyValue(keyValuePair.Key)["Name"].AsString;
							if (asString2 == "Date accessed" || asString2 == "Date modified" || asString2 == "Date created")
							{
								throw ValueException.NewExpressionError<Message0>(Strings.File_SetDatesNotSupported, asRecord, null);
							}
							if (asString2 == "Folder Path")
							{
								text3 = keyValuePair.Value.Invoke(asRecord).AsString;
								bool flag = false;
								try
								{
									flag = Path.IsPathRooted(text3);
								}
								catch (ArgumentException)
								{
								}
								if (!flag)
								{
									throw ValueException.NewExpressionError<Message0>(Strings.Resource_FolderPath_Absolute, asRecord, null);
								}
							}
							else
							{
								if (!(asString2 == "Name"))
								{
									throw ValueException.NewExpressionError<Message0>(Strings.File_UpdateNotSupported, asRecord, null);
								}
								text = keyValuePair.Value.Invoke(asRecord).AsString;
								if (FileHelper.ContainsPathSeparator(text))
								{
									throw ValueException.NewExpressionError<Message0>(Strings.File_NoPathInName, asRecord, null);
								}
							}
						}
						string sourcePath = FileHelper.GetFullPath(text2, asString);
						string destinationPath = FileHelper.GetFullPath(text3, text);
						if (!sourcePath.Equals(destinationPath, StringComparison.CurrentCulture))
						{
							list.Add(ActionValue.New(delegate
							{
								using (this.FolderContentsTable.impersonate())
								{
									FileHelper.MoveDirectory(sourcePath, destinationPath);
								}
								return Value.Null;
							}));
						}
					}
					list.Add(ActionModule.Action.Return.Invoke(NumberValue.New(num)));
					list.Add(new ReturnTypedTableFromCountFunctionValue(new QueryTableValue(this).Type.AsTableType));
					return ActionValue.New(ListValue.New(list)).ClearCache(this.FolderContentsTable.host);
				}

				// Token: 0x06005185 RID: 20869 RVA: 0x00111E80 File Offset: 0x00110080
				public override ActionValue DeleteRows(FunctionValue selector)
				{
					this.FolderContentsTable.VerifyActionPermitted();
					return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.DeleteRows(selector, countOnlyTable));
				}

				// Token: 0x06005186 RID: 20870 RVA: 0x00111EB0 File Offset: 0x001100B0
				private ActionValue DeleteRows(FunctionValue selector, bool countOnlyTable)
				{
					if (!countOnlyTable)
					{
						throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, new QueryTableValue(this), null);
					}
					long num = 0L;
					List<IValueReference> list = new List<IValueReference>();
					foreach (IValueReference valueReference in base.Table.SelectRows(selector))
					{
						num += 1L;
						RecordValue asRecord = valueReference.Value.AsRecord;
						string asString = asRecord["Name"].AsString;
						string asString2 = asRecord["Folder Path"].AsString;
						string fullPath = FileHelper.GetFullPath(asString2, asString);
						if (FileModule.FolderContentsTableValue.FolderContentsQuery.IsFolder(asRecord))
						{
							list.Add(ActionValue.New(delegate
							{
								using (this.FolderContentsTable.impersonate())
								{
									FileHelper.DeleteDirectory(fullPath);
								}
								return Value.Null;
							}));
						}
						else
						{
							list.Add(ActionValue.New(delegate
							{
								using (this.FolderContentsTable.impersonate())
								{
									FileHelper.DeleteFile(fullPath);
								}
								return Value.Null;
							}));
						}
					}
					list.Add(ActionModule.Action.Return.Invoke(NumberValue.New(num)));
					list.Add(new ReturnTypedTableFromCountFunctionValue(new QueryTableValue(this).Type.AsTableType));
					return ActionValue.New(ListValue.New(list)).ClearCache(this.FolderContentsTable.host);
				}

				// Token: 0x06005187 RID: 20871 RVA: 0x00111FF4 File Offset: 0x001101F4
				private static bool IsFolder(RecordValue entry)
				{
					Value value;
					Value value2;
					Value value3;
					return (entry.TryGetValue("Attributes", out value) && value.AsRecord.TryGetValue("Directory", out value2) && value2.AsBoolean) || (entry.TryGetValue("Content", out value3) && value3.IsTable);
				}

				// Token: 0x06005188 RID: 20872 RVA: 0x00112048 File Offset: 0x00110248
				private static string FieldOrDefault(RecordValue record, string field, string defaultString)
				{
					Value value;
					if (record.TryGetValue(field, out value))
					{
						return value.AsString;
					}
					return defaultString;
				}

				// Token: 0x04002C71 RID: 11377
				private static readonly TableValue EmptyContentTable = TableModule.Table.FromRows.Invoke(ListValue.Empty, FileHelper.FolderResultTypeValue(FileHelper.FolderOptions.EnumerateFoldersAndFilesDeep)).AsTable;
			}

			// Token: 0x02000B95 RID: 2965
			private sealed class FolderContentsEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x06005199 RID: 20889 RVA: 0x001122D8 File Offset: 0x001104D8
				public FolderContentsEnumerator(IEngineHost host, string folderPath, OptionsRecord optionsRecord, Func<IDisposable> impersonate, FunctionValue localFilter, IEnumerator<FileHelper.FileData> fileEnumerator)
				{
					this.host = host;
					this.folderPath = folderPath;
					this.optionsRecord = optionsRecord;
					this.impersonate = impersonate;
					this.localFilter = localFilter;
					this.fileEnumerator = fileEnumerator;
					this.aceCache = new FileHelper.AccessControlEntriesCache();
					IConfigurationPropertyService configurationPropertyService = this.host.QueryService<IConfigurationPropertyService>();
					object obj;
					if (configurationPropertyService != null && configurationPropertyService.Values.TryGetValue("IsMacSandboxUIEnabled", out obj))
					{
						bool? flag = obj as bool?;
						bool flag2 = true;
						if ((flag.GetValueOrDefault() == flag2) & (flag != null))
						{
							this.isMacSandboxUIEnabled = true;
						}
					}
				}

				// Token: 0x17001952 RID: 6482
				// (get) Token: 0x0600519A RID: 20890 RVA: 0x0011236F File Offset: 0x0011056F
				public IValueReference Current
				{
					get
					{
						return this.current;
					}
				}

				// Token: 0x17001953 RID: 6483
				// (get) Token: 0x0600519B RID: 20891 RVA: 0x00112377 File Offset: 0x00110577
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x0600519C RID: 20892 RVA: 0x0011237F File Offset: 0x0011057F
				public void Dispose()
				{
					this.current = null;
					if (this.fileEnumerator != null)
					{
						this.fileEnumerator.Dispose();
						this.fileEnumerator = null;
					}
				}

				// Token: 0x0600519D RID: 20893 RVA: 0x001123A4 File Offset: 0x001105A4
				public bool MoveNext()
				{
					FileHelper.FileData fileData = null;
					bool flag;
					try
					{
						while (this.fileEnumerator.MoveNext())
						{
							fileData = this.fileEnumerator.Current;
							this.current = FileModule.FolderContentsRecordValue.New(this.host, this.aceCache, fileData, this.optionsRecord, this.impersonate);
							if (this.localFilter == null || this.localFilter.Invoke(this.current).AsBoolean)
							{
								return true;
							}
							this.current = null;
						}
						flag = false;
					}
					catch (UnauthorizedAccessException obj) when (FileHelper.ShouldConvertUnauthorizedAccess(this.folderPath, this.isMacSandboxUIEnabled))
					{
						throw FileHelper.NewUnauthorizedAccessException(this.host, this.folderPath, fileData);
					}
					catch (Exception ex)
					{
						string text = ((fileData == null) ? this.folderPath : fileData.FullName);
						throw FileErrors.HandleException(ex, TextValue.New(text));
					}
					return flag;
				}

				// Token: 0x0600519E RID: 20894 RVA: 0x00112498 File Offset: 0x00110698
				public void Reset()
				{
					this.current = null;
					this.fileEnumerator.Reset();
				}

				// Token: 0x04002C81 RID: 11393
				private bool isMacSandboxUIEnabled;

				// Token: 0x04002C82 RID: 11394
				private readonly string folderPath;

				// Token: 0x04002C83 RID: 11395
				private readonly IEngineHost host;

				// Token: 0x04002C84 RID: 11396
				private readonly Func<IDisposable> impersonate;

				// Token: 0x04002C85 RID: 11397
				private readonly FunctionValue localFilter;

				// Token: 0x04002C86 RID: 11398
				private readonly FileHelper.AccessControlEntriesCache aceCache;

				// Token: 0x04002C87 RID: 11399
				private readonly OptionsRecord optionsRecord;

				// Token: 0x04002C88 RID: 11400
				private RecordValue current;

				// Token: 0x04002C89 RID: 11401
				private IEnumerator<FileHelper.FileData> fileEnumerator;
			}
		}

		// Token: 0x02000B96 RID: 2966
		private sealed class FolderFilesFunctionValue : NativeFunctionValue2<TableValue, TextValue, Value>
		{
			// Token: 0x0600519F RID: 20895 RVA: 0x001124AC File Offset: 0x001106AC
			public FolderFilesFunctionValue(IEngineHost host)
				: base(FileHelper.FolderResultTypeValue(FileHelper.FolderOptions.EnumerateFilesDeep), 1, "path", TypeValue.Text, "options", FileModule.FolderFilesFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x17001954 RID: 6484
			// (get) Token: 0x060051A0 RID: 20896 RVA: 0x001124D6 File Offset: 0x001106D6
			public override IFunctionIdentity FunctionIdentity
			{
				get
				{
					return new FunctionTypeIdentity(typeof(FileModule.FolderFilesFunctionValue));
				}
			}

			// Token: 0x060051A1 RID: 20897 RVA: 0x001124E8 File Offset: 0x001106E8
			public override TableValue TypedInvoke(TextValue path, Value options)
			{
				IResource resource = FileModule.FolderFilesFunctionValue.GetResource(path);
				Func<IDisposable> func = FileHelper.VerifyPermissionAndGetImpersonationWrapper(this.host, resource);
				string nonNormalizedPath = resource.NonNormalizedPath;
				OptionsRecord optionsRecord = FileModule.FolderOptionRecord.CreateOptions("Folder.Files", options);
				return FileModule.FolderContentsTableValue.New(this.host, nonNormalizedPath, optionsRecord, func, "*", null, FileHelper.FolderOptions.EnumerateFilesDeep);
			}

			// Token: 0x17001955 RID: 6485
			// (get) Token: 0x060051A2 RID: 20898 RVA: 0x00110B06 File Offset: 0x0010ED06
			public override string PrimaryResourceKind
			{
				get
				{
					return "Folder";
				}
			}

			// Token: 0x060051A3 RID: 20899 RVA: 0x00112538 File Offset: 0x00110738
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				foundOptions = RecordValue.Empty;
				unknownOptions = Keys.Empty;
				Dictionary<string, IExpression> dictionary;
				Value value;
				if (!FileModule.FolderFilesFunctionValue.pattern.TryMatch(expression, out dictionary) || !dictionary.TryGetConstant("path", out value))
				{
					location = null;
					return false;
				}
				Value value2;
				IResource resource;
				RecordValue recordValue;
				DataSourceLocationFactory dataSourceLocationFactory;
				if (dictionary.TryGetConstant("filename", out value2))
				{
					Value value3;
					if (!dictionary.TryGetConstant("folderpath", out value3))
					{
						location = null;
						return false;
					}
					resource = Resource.New("File", Path.Combine(value3.AsString, value2.AsString));
					recordValue = RecordValue.Empty;
					dataSourceLocationFactory = FileDataSourceLocation.Factory;
				}
				else
				{
					resource = Resource.New("Folder", value.AsString);
					recordValue = RecordValue.New(Keys.New("HierarchicalNavigation"), new Value[] { LogicalValue.True });
					dataSourceLocationFactory = FolderDataSourceLocation.Factory;
				}
				if (!dataSourceLocationFactory.TryCreateFromResource(resource, false, out location))
				{
					return false;
				}
				IExpression expression2;
				if (dictionary.TryGetValue("options", out expression2))
				{
					RecordValue record = ExpressionAnalysis.GetRecord(expression2);
					if (record == null)
					{
						foundOptions = null;
						unknownOptions = null;
					}
					else
					{
						foundOptions = ExpressionAnalysis.RemovePlaceholders(record, out unknownOptions).Concatenate(recordValue).AsRecord;
					}
				}
				else
				{
					foundOptions = recordValue;
				}
				return true;
			}

			// Token: 0x060051A4 RID: 20900 RVA: 0x00110C3C File Offset: 0x0010EE3C
			private static IResource GetResource(TextValue path)
			{
				return Resource.New("Folder", FileModule.LocalPath(path.AsString));
			}

			// Token: 0x04002C8A RID: 11402
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__path, _o_options)", "__func(__path, _o_options){[Name=__filename, Folder Path=__folderpath]}[Content]" });

			// Token: 0x04002C8B RID: 11403
			private static readonly TypeValue optionsType = FileModule.FolderOptionRecord.CreateRecordType().Nullable;

			// Token: 0x04002C8C RID: 11404
			private readonly IEngineHost host;
		}
	}
}
