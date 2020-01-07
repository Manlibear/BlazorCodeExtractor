A dotnet CLI command to extract Blazor @code blocks and add them back in to the page markup for display.

This is a partial hack to help answer https://stackoverflow.com/questions/59593826/how-to-print-code-section-of-a-razor-page-to-the-html-view.

## Modify Program

Don't use as-is, this is a demo hack. Review program.cs and adjust to suit your needs. This appends your @code block as an HTML <code> block **each time it runs**, you don't want this and need to adjust. What you do with your sexy new <code> is up to you.

## Build and Install Locally

From ~/src/BlazorCodeExtractor folder:

```
dotnet pack
dotnet tool install --global --add-source ./nupkg BlazorCodeExtractor
```

## Run on Blazor Project

Navigate to client-side Blazor project:

```
extract-blazor-code
```

This finds all .razor files, extracts @code blocks, and appends them back in within an HTML code block.

For example, if you have home.razor:

```
@code {
  private string _name = "Andy";
}
```

After running command you'll get:

```
@code {
  private string _name = "Andy";
}

<code>
@@code {
  private string _name = "Andy";
}
</code>
```
