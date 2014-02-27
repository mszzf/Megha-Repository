package com.example.Serene;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.widget.TextView;

/**
 * Created by ranger on 2/22/14.
 */
public class DisplayMessageActivity extends Activity {
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_dispaly_message);
        Intent intent = getIntent();
        String message = intent.getStringExtra("editMessage");

        TextView textView = new TextView(this);
        textView.setTextSize(40);
        textView.setText(message);

        setContentView(textView);
    }
}