A dotnet CLI command to extract Blazor @code blocks and save them as markdown alongside the original file.

This is a quick-and-dirty project to help answer https://stackoverflow.com/questions/59593826/how-to-print-code-section-of-a-razor-page-to-the-html-view.

If you start with a simple **Index.razor** page:

```
<h1>Hello world!</h1>

@code {
  private string _name = "Andy";
}
```

...and run `extract-blazor-code` either manually or as a build task it will generate **Index.razor.md** containing just your code block and its content.

# Instructions

## Modify Program

Don't use as-is, this is legit demoware. Review program.cs and adjust to suit your needs. 

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

This finds all .razor files, extracts @code blocks, and generates markdown files with only the @code. 
