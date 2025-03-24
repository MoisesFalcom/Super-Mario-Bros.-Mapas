// Autor: Moises Falcón Pacheco
// Matrícula: A01801140
// Controlador principal del menú: gestiona la interfaz de inicio y la ventana de ayuda.

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    // Contenedores visuales para secciones del menú
    private VisualElement MenuContainer;
    private VisualElement HelpContainer;

    private void Start()
    {
        InitializeInterface();
        ConfigureMenuActions();
        ConfigureHelpActions();
    }

    // Inicializa los elementos visuales desde el documento UI
    private void InitializeInterface()
    {
        var Root = GetComponent<UIDocument>().rootVisualElement;
        MenuContainer = Root.Q<TemplateContainer>("menu");
        HelpContainer = Root.Q<TemplateContainer>("ayuda");
    }

    // Enlaza eventos a los botones del menú principal
    private void ConfigureMenuActions()
    {
        var View = new MainMenuView(MenuContainer);
        View.OnHelpClicked = () => SetMenuVisibility(false);
        View.OnStartClicked = LoadGameScene;
        View.OnCreditsClicked = LoadCreditsScene;
        View.OnExitClicked = CloseApplication;
    }

    // Enlaza eventos a los botones de la sección de ayuda
    private void ConfigureHelpActions()
    {
        var HelpView = new HelpSection(HelpContainer);
        HelpView.OnBackClicked = () => SetMenuVisibility(true);
    }

    // Alterna visibilidad entre menú principal y ayuda
    private void SetMenuVisibility(bool showMenu)
    {
        MenuContainer.ToggleDisplay(showMenu);
        HelpContainer.ToggleDisplay(!showMenu);
    }

    // Carga la escena del juego
    private void LoadGameScene() => SceneManager.LoadScene("Game");

    // Carga la escena de créditos
    private void LoadCreditsScene() => SceneManager.LoadScene("Credits");

    // Cierra la aplicación
    private void CloseApplication() => Application.Quit();
}

// Extensión para mostrar u ocultar VisualElements
public static class VisualElementExtensions
{
    public static void ToggleDisplay(this VisualElement element, bool isVisible)
    {
        if (element != null)
            element.style.display = isVisible ? DisplayStyle.Flex : DisplayStyle.None;
    }
}
