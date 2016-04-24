#!/usr/bin/env ruby

require "webrick"
require "rubygems"
require "active_record"

ActiveRecord::Base.establish_connection(
  :adapter=> "mysql",
  :host => "localhost",
  :username => "root",
  :password => "",
  :database=> "test"
)

class TreeNode < ActiveRecord::Base
end

def foobar
    {
        :id => 1,
        :name => "root",
        :description => "description",
        :children => [
            {
                :id => 2,
                :name => "level1",
                :description => "description",
                :children => [
                    {
                        :id => 4,
                        :name => "level22222",
                        :description => "This is level2",
                    },
                    {
                        :id => 5,
                        :name => "level222",
                        :description => "This is level2",
                        :children => [
                            {
                                :id => 10,
                                :name => "level333",
                                :description => "Here are a few key resources to get you started with building mobile apps quickly",
                                :children => [
                                    {
                                        :id => 11,
                                        :name => "level444444",
                                        :description => "Introduces Android development. Covers the tool chain, Xamarin.Android projects, and Android fundamentals."
                                    },
                                    {
                                        :id => 12,
                                        :name => "level 4444",
                                        :description => "This guide walks through the installation steps and configuration details required to install Xamarin.Android on Windows."
                                    },
                                ]
                            }
                        ]
                    }
                ]
            },
            {
                :id => 3,
                :name => "level1111",
                :description => "description",
                :children => [
                    {
                        :id => 6,
                        :name => "level2222222",
                        :description => "Never know this level: level2, 233333"
                    }
                ]
            },
            {
                :id => 3,
                :name => "level111111111",
                :description => "description",
                :children => [
                    {
                        :id => 7,
                        :name => "level22222222222",
                        :description => "blahblahblah"
                    },
                    {
                        :id => 8,
                        :name => "level222",
                        :description => "The AppDelegate.cs file contains our AppDelegate class",
                    },
                    {
                        :id => 9,
                        :name => "level two",
                        :description => "which is responsible for creating our window and listening to OS events"
                    },
                ]
            }
        ]
    }
end

class MyServlet < WEBrick::HTTPServlet::AbstractServlet
    def do_GET (request, response)
        if request.query["root_id"]
            response.status = 200
            response.content_type = "application/json"
            response.body = foobar.to_json + "\n"
        else
            response.status = 404
            response.body = "Do not provide the root id"
        end
    end
end

server = WEBrick::HTTPServer.new(:Port => 8022)

server.mount "/", MyServlet

trap("INT") {
    server.shutdown
}

server.start
