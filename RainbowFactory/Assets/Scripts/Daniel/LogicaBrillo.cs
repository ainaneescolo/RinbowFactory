using UnityEngine;
using UnityEngine.UI;

public class LogicaBrillo : MonoBehaviour
{
    public Slider slider;  // El deslizador para ajustar el brillo
    public Image uiBackground; // El elemento de la interfaz de usuario que quieres ajustar

    private void Start()
    {
        // Cargar el valor de brillo desde PlayerPrefs (si existe)
        if (PlayerPrefs.HasKey("Brillo"))
        {
            float brilloGuardado = PlayerPrefs.GetFloat("Brillo");
            slider.value = brilloGuardado;
            CambiarBrillo(brilloGuardado);
        }
    }

    // M�todo para cambiar el brillo
    public void CambiarBrillo(float valor)
    {
        // Ajustar el brillo de la escena
        //RenderSettings.ambientIntensity = valor;

        // Ajustar el brillo de la interfaz de usuario (ajusta el color de fondo)
        Color nuevoColor = uiBackground.color;
        nuevoColor.a = valor; // Cambia la opacidad de la imagen
        uiBackground.color = nuevoColor;

        // Guardar el valor en PlayerPrefs
        PlayerPrefs.SetFloat("Brillo", valor);
        PlayerPrefs.Save();
    }
}