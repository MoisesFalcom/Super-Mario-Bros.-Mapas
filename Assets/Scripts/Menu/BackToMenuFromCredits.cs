// Autor: Moises Falcón Pacheco
// Matrícula: A01801140
// Controlador del botón para regresar al menú desde la escena de créditos.

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BackToMenuFromCredits : MonoBehaviour
{
    // Referencia al botón de regreso en la interfaz
    private Button BtnReturn;

    private void Start()
    {
        // Se obtiene el elemento raíz del documento UI
        var Root = GetComponent<UIDocument>().rootVisualElement;

        // Se busca el botón llamado "regresar"
        BtnReturn = Root.Q<Button>("regresar");

        // Se asigna el evento al botón para cargar la escena del menú principal
        BtnReturn?.RegisterCallback<ClickEvent>(evt => SceneManager.LoadScene("MainMenu"));
    }
}
