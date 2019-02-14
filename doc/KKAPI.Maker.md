## `ChaFileLoadedEventArgs`

```csharp
public class KKAPI.Maker.ChaFileLoadedEventArgs
    : EventArgs

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | Body |  | 
| `ChaFileControl` | CharacterInstance |  | 
| `Boolean` | Coordinate |  | 
| `Boolean` | Face |  | 
| `String` | Filename |  | 
| `Boolean` | Hair |  | 
| `ChaFile` | LoadedChaFile | Use this to get extended data on the character | 
| `Boolean` | Parameter |  | 
| `Byte` | Sex |  | 


## `CharacterLoadFlags`

Specifies which parts of the character will be loaded when loading a card in character maker.  (It's the toggles at the bottom of load window) Only includes stock toggles.
```csharp
public class KKAPI.Maker.CharacterLoadFlags

```

Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | Body |  | 
| `Boolean` | Clothes |  | 
| `Boolean` | Face |  | 
| `Boolean` | Hair |  | 
| `Boolean` | Parameters |  | 


## `MakerAPI`

Provides a way to add custom items to the in-game Character Maker, and gives useful methods for interfacing with the maker.
```csharp
public static class KKAPI.Maker.MakerAPI

```

Static Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | CharaListIsLoading | Use to avoid unnecessary processing cards when they are loaded to the character list.  For example, don't load extended data for these characters since it's never used. | 
| `Boolean` | InsideAndLoaded | Maker is fully loaded and running | 
| `Boolean` | InsideMaker | The maker scene is currently loaded. It might still be loading! | 
| `ChaFile` | LastLoadedChaFile | ChaFile of the character currently opened in maker. Do not use to save extended data, or it will be lost when saving the card.  Use ChaFile from <code>ExtendedSave.CardBeingSaved</code> event to save extended data instead. | 


Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `T` | AddControl(`T` control) | Add custom controls. If you want to use custom sub categories, register them by calling AddSubCategory. | 
| `void` | AddSubCategory(`MakerCategory` category) | Add custom sub categories. They need to be added before maker starts loading,  or in the `KKAPI.Maker.MakerAPI.RegisterCustomSubCategories` event. | 
| `ChaControl` | GetCharacterControl() | Get the ChaControl of the character serving as a preview in character maker.  Outside of character maker and early on in maker load process this returns null. | 
| `CharacterLoadFlags` | GetCharacterLoadFlags() | Get values of the default partial load checkboxes present at the bottom of the  character load window (load face, body, hair, parameters, clothes).  Returns null if the values could not be collected (safe to assume it's the same as being enabled). | 
| `CoordinateType` | GetCurrentCoordinateType() | Currently selected maker coordinate | 
| `CustomBase` | GetMakerBase() | Returns current maker logic instance.  Same as `Singleton`1.Instance` | 
| `Int32` | GetMakerSex() | 0 is male, 1 is female | 
| `void` | Init(`Boolean` insideStudio) |  | 


Static Events

| Type | Name | Summary | 
| --- | --- | --- | 
| `EventHandler<ChaFileLoadedEventArgs>` | ChaFileLoaded | Fired when the current ChaFile in maker is being changed by loading other cards or coordinates.  This event is only fired when inside the character maker. It's best used to update the interface with new values.    You might need to wait for the next frame with `UnityEngine.MonoBehaviour.StartCoroutine(System.Collections.IEnumerator)` before handling this. | 
| `EventHandler` | InsideMakerChanged | Firen whenever `KKAPI.Maker.MakerAPI.InsideMaker` changes. This is the earliest event fired when user starts the character maker. | 
| `EventHandler<RegisterCustomControlsEvent>` | MakerBaseLoaded | Maker is fully loaded. Use to load mods that rely on something that is loaded late, else use MakerStartedLoading.  This is the last chance to add custom controls!  Warning: All custom subcategories and custom controls are cleared on maker exit and need to be re-added on next maker  start. | 
| `EventHandler` | MakerExiting | Fired after the user exits the maker. Use this to clean up any references and resources.  You want to return to the state you were in before maker was loaded. | 
| `EventHandler` | MakerFinishedLoading | Maker is fully loaded and the user has control.  Warning: Avoid loading mods or doing anything heavy in this event, use EarlyMakerFinishedLoading instead. | 
| `EventHandler<RegisterCustomControlsEvent>` | MakerStartedLoading | Early in the process of maker loading. Most game components are initialized and had their Start methods ran.  Warning: Some components and objects might not be loaded or initialized yet, especially if they are mods.  Warning: All custom subcategories and custom controls are cleared on maker exit and need to be re-added on next maker  start. | 
| `EventHandler<RegisterSubCategoriesEvent>` | RegisterCustomSubCategories | This event is fired every time the character maker is being loaded, near the very beginning.  This is the only chance to add custom sub categories. Custom controls can be added now on later in `KKAPI.Maker.MakerAPI.MakerBaseLoaded`.  Warning: All custom subcategories and custom controls are cleared on maker exit and need to be re-added on next maker start.  It's recommended to completely clear your GUI state in `KKAPI.Maker.MakerAPI.MakerExiting` in preparation for loading into maker again. | 


## `MakerCategory`

Specifies a category inside character maker.
```csharp
public class KKAPI.Maker.MakerCategory

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | CategoryName | Main category gameObject name. Main categories are the square buttons at the top-left edge of the screen.  They contain multiple subcategories (tabs on the left edge of the screen). | 
| `String` | DisplayName | The text displayed on the subcategory tab on the left edge of the screen. | 
| `Int32` | Position | Numeric position of the subcategory.  When making new subcategories you can set this value to be in-between stock subcategories. | 
| `String` | SubCategoryName | Sub category gameObject name. Sub categories are the named tabs on the left edge of the screen.  They contain the actual controls (inside the window on the right of the tabs). | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | Equals(`Object` obj) |  | 
| `Int32` | GetHashCode() |  | 
| `String` | ToString() | Get combined name for logging etc. | 


## `MakerConstants`

Useful values from the character maker. Mostly built-in categories for use with registering custom controls.
```csharp
public static class KKAPI.Maker.MakerConstants

```

Static Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `IEnumerable<MakerCategory>` | BuiltInCategories | All ategories that are built-into the character maker by default. | 


Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | <GetBuiltInCategory>g__MakeKey|4_0(`String` catName, `String` subCatName) |  | 
| `MakerCategory` | GetBuiltInCategory(`String` category, `String` subCategory) | Quick search for a built-in category. If you know what category you want to use at  compile time you can use the shortcuts instead, e.g. `KKAPI.Maker.MakerConstants.Face.Ear` | 


## `RegisterCustomControlsEvent`

```csharp
public class KKAPI.Maker.RegisterCustomControlsEvent
    : EventArgs

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `T` | AddControl(`T` control) | Add custom controls. If you want to use custom sub categories, register them by calling AddSubCategory. | 
| `MakerCoordinateLoadToggle` | AddCoordinateLoadToggle(`MakerCoordinateLoadToggle` toggle) |  | 
| `MakerLoadToggle` | AddLoadToggle(`MakerLoadToggle` toggle) | Add a toggle to the bottom of the "Load character" window that allows for partial loading of characters. | 


## `RegisterSubCategoriesEvent`

```csharp
public class KKAPI.Maker.RegisterSubCategoriesEvent
    : RegisterCustomControlsEvent

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | AddSubCategory(`MakerCategory` category) | Add custom sub categories. They need to be added before maker starts loading,  or in the RegisterCustomSubCategories event. | 

