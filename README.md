# ESP32 2.4 inch TFT display driver
A ESP32 TFT board with USB Type C and additional headers for adding sensors.

For any questions make, sure to contact me. You can also DM me at @wired_less_maker
.

![Test Image 2](https://raw.githubusercontent.com/sebastianttr/ESP32_TFT/master/ESP32_TFT.PNG)
# Parts List

This project requires several components: 

* ESP32 WROOM module
  * Preferably a ESP32U or a ESP32S, because of the ability to add an external antenna for better wireless performance
  ![Test Image 4](https://imgaz2.staticbg.com/thumb/large/oaupload/banggood/images/C8/31/c5a4ec89-d064-4ed9-90bc-5f5fa804050a.jpg)
* USB Type C Receptable
  * Make sure you select the correct receptable, this is the footprint
  ![USBC](https://raw.githubusercontent.com/sebastianttr/ESP32_TFT/master/USBC.PNG)

* CH340 USB to TTL Converter IC
* LD1117 3V3 Regulator IC
* [OPTIONAL] RFM95 for LoRaWAN
   * If you decide on using LoRaWAN, make sure to also get a U.FL antenna connector 
* 2 SMD Buttons (6 mm X 3.9 mm)
* Passive components
   * 4x 10kOhm 0805 Resistor
   * 2x 5.1kOhm 0805 Resistor 
   * 2x 12kOhm 0805 Resistor
   * 1x Schottky Diode 1206 (the choice is yours)
   * 2x 100nF 0805 Capacitor
   * 1x 10uF 1206 Capacitor
   * 2x 22pF 0603 Capacitor 
   * 1x 12Mhz SMD Crystal 5.0 mm x 3.2 mm
     
     * You will need this configuration. Make sure to look into the datasheet 
       ![Crystal](https://raw.githubusercontent.com/sebastianttr/ESP32_TFT/master/Crystal.PNG)
   
   * 2x SOT-23-3 BJT Tranistor of either types: MMBT1000 or MMBT2222
     * S8050 is also OK
* LED
  * For LED, you can use whatever you like.
  * The LED Pin outputs 3.3V from the ESP32, in case you use a LED with a forward voltage lower than ~ 3.3V, you can calculate and add your own resistor at R10
  
# Before usage
  * Before using the device with the TFT touchscreen, make sure to jump these 2 pins above the touchscreen connector: 
  ![LED_TOUCH](https://raw.githubusercontent.com/sebastianttr/ESP32_TFT/master/LED_TOUCH.png)
  
  These pins exist to set the brightness of the backlight. In this case, we connect the LED- pin from the touchscreen with 3.3V.
